using AutoMapper;
using GSF.Application.Security.Roles.Queries.GetRoleCrudQuery;
using GSF.Application.Security.Roles.Queries.GetTree;
using GSFSharedResources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Socios.Web.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Socios.Web.Areas.Security.Pages.Roles;


public class RoleCrudModel : BasePageModel
{
    protected const string _branchPriority = "BRANCH_PRIORITY";
    protected const string _leafPriority = "LEAF_PRIORITY";
    protected const string _optionType = "Option";
    protected const string _grantType = "Grant";
    protected const string _separator = ",";

    [BindProperty] public string Mode { get; set; } = "";
    [BindProperty] public long Id { get; set; }
    [Display(Name = "Nombre")]
    [BindProperty] public string Name { get; set; }
    [Display(Name = "Descripción")]
    [BindProperty] public string Description { get; set; }
    [BindProperty] public DateTime Modified { get; set; }
    [HiddenInput][BindProperty] public byte[] RowVersion { get; set; } = new byte[] { 0 }; //Must be initializated, if not the binding in the create page (using a partialView) will have a modelState invalid.
    [Display(Name = "Grupo Dueño")]
    [BindProperty] public string GroupOwnerName { get; set; }
    [BindProperty] public bool SystemUse { get; set; }

    //[BindProperty] public List<OptionGrantTreeDto> Options { get; set; }
    //[BindProperty] public OptionGrantTreeDto SingleOption { get; set; }
    [BindProperty] public string SelectedValues { get; set; }
    [BindProperty] public List<OptionTreeNode> Children { get; set; } = new List<OptionTreeNode>();
    [BindProperty] public string TreeOptions { get; set; }

    public HashSet<long> SelectedOptions { get; set; } = new HashSet<long>();
    public HashSet<long> SelectedGrants { get; set; } = new HashSet<long>();
    private string SavedValues { get; set; }

    public long CurrentGroupId { get; set; }


    protected readonly IMapper _mapper;
    protected readonly IStringLocalizer<Shared> _loc;

    public RoleCrudModel(IMapper mapper,
                         IStringLocalizer<Shared> loc)
    {
        _mapper = mapper;
        _loc = loc;
    }

    public async Task<IActionResult> LoadRole(int id)
    {
        IActionResult result;
        var query = new GetRoleCrudQuery() { Id = id };
        RoleCrudDto roleDto = await Mediator.Send(query);

        //Ver si cambiamos para que la validacion en la query y devuelva una excepcion.
        if (roleDto == null)
        {
            ErrorMessage = _loc["El rol ya no existe"];
            result = RedirectByModelState("/Roles/Index", new { area = "Security" });
        }
        else
        {
            _mapper.Map(roleDto, this);
            CompleteSelectedValuesFromDto(roleDto.RoleOptionsIds, roleDto.RolesGrantsIds);
            result = Page();
        }
        return result;
    }

    /// <summary>
    /// Search the Nodes to create obtain the SelectedValues in the same format used to display the info.
    /// </summary>
    /// <param name="OptionsIds">List of Ids of Options.</param>
    /// <param name="GrantsIds">List of Ids of Grants.</param>
    private void CompleteSelectedValuesFromDto(List<long> OptionsIds, List<long> GrantsIds)
    {
        List<string> idsList = new List<string>();

        SavedValues = string.Join(_separator, OptionsIds.Select(id => id + "-" + _optionType));
        if (string.IsNullOrEmpty(SavedValues))
            SavedValues += string.Join(_separator, GrantsIds.Select(id => id + "-" + _grantType));
        else
            SavedValues += _separator + string.Join(_separator, GrantsIds.Select(id => id + "-" + _grantType));

        string[] values = SavedValues.Split(_separator);

        foreach (string val in values)
        {
            if (CheckSubNodesForSelectedId(val))
                idsList.Add(val);
        }

        SelectedValues = string.Join(_separator, idsList);
    }

    /// <summary>
    /// Search the Node with the indicated id and search if the all its SubNodes are selected too.
    /// </summary>
    /// <param name="id">Value to search.</param>
    /// <returns>Whether the Node with the indicated id has all its SubNodes are selected.</returns>
    private bool CheckSubNodesForSelectedId(string id)
    {
        bool result;
        OptionTreeNode node = RecursiveFindSingleNodeById(Children, id);

        //If not exist its because the relationship between the current user and Option/Grant no longer exists, so its havent been loaded by the query that searchs the OptionTree.
        if (node != null)
            result = CheckAllChildSelected(node);
        else
            result = false;
        return result;
    }

    /// <summary>
    /// Given a Node check if all of its SubNodes are selected.
    /// </summary>
    /// <param name="node">Node to check.</param>
    /// <returns>Whether the Node passed as parameter has all its SubNodes are selected.</returns>
    private bool CheckAllChildSelected(OptionTreeNode node)
    {
        bool result = true;
        bool subNodesChecked = false;
        int i = 0;
        while (node.Children != null && i < node.Children.Count && result)
        {
            subNodesChecked = true;
            var subNode = node.Children[i];
            string[] values = SavedValues.Split(_separator);

            if (values.Contains(subNode.Id))
            {
                if (subNode.Children != null && subNode.Children.Any())
                {
                    int j = 0;
                    while (j < subNode.Children.Count && result)
                    {
                        result = CheckAllChildSelected(subNode.Children[j]);
                        j++;
                    }
                }
            }
            else
            {
                result = false;
            }
            i++;
        }

        if (!subNodesChecked)
        {
            string[] values = SavedValues.Split(_separator);
            result = values.Contains(node.Id);
        }

        return result;
    }

    /// <summary>
    /// Find the Node with the indicated Id.
    /// </summary>
    /// <param name="optionsList">List of OptionTreeNode to search through.</param>
    /// <param name="id">Value to search the Nodes for.</param>
    /// <returns></returns>
    private OptionTreeNode RecursiveFindSingleNodeById(List<OptionTreeNode> optionsList, string id)
    {
        OptionTreeNode result = null;
        int i = 0;
        while (i < optionsList.Count && result == null)
        {
            OptionTreeNode node = optionsList[i];
            if (node.Id == id)
            {
                result = node;
            }
            else if (node.Children != null && node.Children.Any())
            {
                result = RecursiveFindSingleNodeById(node.Children, id);
            }
            i++;
        }
        return result;
    }

    /// <summary>
    /// Generate the data for the OptionTree.
    /// </summary>
    /// <returns></returns>
    public async Task GenerateOptionTree()
    {
        var optionsQuery = new GetTreeQuery();
        List<OptionGrantTreeDto> Options = await Mediator.Send(optionsQuery);
        if (Options != null && Options.Any())
            RecursiveNodesProcessing(Options, Children);

        //We use this settings to prevent de serializations of empty lists.
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
        };
        TreeOptions = JsonConvert.SerializeObject(Children, settings);
    }

    /// <summary>
    /// Recursively loop through the retrieved Options, creating the nodes for the OptionTree.
    /// </summary>
    /// <param name="options">List of OptionsGrants to loop thourgh.</param>
    /// <param name="children">Node where child nodes will be inserted.</param>
    private void RecursiveNodesProcessing(List<OptionGrantTreeDto> options, List<OptionTreeNode> children)
    {
        foreach (OptionGrantTreeDto option in options)
        {
            children.Add(CreateOptionChildNode(option));
        }
    }

    /// <summary>
    /// Creates a childNode from an Option.
    /// </summary>
    /// <param name="element">Option from wich the new node will be created.</param>
    /// <returns></returns>
    private OptionTreeNode CreateOptionChildNode(OptionGrantTreeDto element)
    {
        var newNode = new OptionTreeNode
        {

            Id = element.Id + "-" + element.Type,
            Label = element.Name,
            Type = element.Type,
            IsDisabled = element.IsDisabled,
            ParentId = element.ParentId,
            EntityId = element.Id
        };
        if (element.Options != null && element.Options.Any())
        {
            newNode.Children = new List<OptionTreeNode>();
            newNode.Label = getFolderEmoji() + newNode.Label;
            RecursiveNodesProcessing(element.Options, newNode.Children);
        }
        else
            newNode.Label = newNode.Type == _optionType ? getClipBoardEmoji() + newNode.Label : getKeyEmoji() + newNode.Label;
        return newNode;
    }

    private string getKeyEmoji()
    {
        return "🔑";
    }

    private string getClipBoardEmoji()
    {
        return "📋";
    }

    private string getFolderEmoji()
    {
        return "📁";
    }

    /// <summary>
    /// Generate the List of OptionsIds and List of GrantsIds to be saved.
    /// </summary>
    public void GenerateListsForSave()
    {
        Children = JsonConvert.DeserializeObject<List<OptionTreeNode>>(TreeOptions);

        List<OptionTreeNode> result = FindNodesAndSubNodesByIds();

        foreach (OptionTreeNode element in result)
        {
            if (element.Type == _grantType)
            {
                SelectedGrants.Add(element.EntityId);
                SelectedOptions.Add(element.ParentId);
            }
            else
            {
                SelectedOptions.Add(element.EntityId);
                if (element.ParentId != 0)
                //if (element.parentId != null)
                {
                    SelectedOptions.Add(element.ParentId);
                }
            }
            OptionTreeNode originNode = FindOriginTreeNodeRecursive(Children, element.ParentId);
            if (originNode != null)
                SelectedOptions.Add(originNode.EntityId); //Always add, since its a HashSet if the value is repeated wont be added.
        }
    }

    /// <summary>
    /// Searches through the treet until the parentNode with no parent is found.
    /// </summary>
    /// <param name="tree">List<OptionTreeNode> to search for inner nodes.</param>
    /// <param name="parentId">Id to find.</param>
    /// <returns></returns>
    private OptionTreeNode FindOriginTreeNodeRecursive(List<OptionTreeNode> tree, long parentId)
    {
        OptionTreeNode result = null;

        int i = 0;

        while (i < tree.Count && result == null)
        {
            var node = tree[i];
            if (node.EntityId != parentId)
            {
                if (node.Children != null)
                {
                    result = FindOriginTreeNodeRecursive(node.Children, parentId);
                    if (result != null && result.ParentId != default)
                        result = FindOriginTreeNodeRecursive(Children, result.ParentId);
                }
            }
            else
                if (node.Type == "Option")
                result = node;
            i++;
        }

        return result;
    }

    /// <summary>
    /// Find the nodes (and its sub nodes) from the SelectedValues.
    /// </summary>
    /// <returns>The Node with the Id indicated in the SelectedValues field and its sub nodes.</returns>
    private List<OptionTreeNode> FindNodesAndSubNodesByIds()
    {
        List<OptionTreeNode> selectedNodes = new List<OptionTreeNode>();

        var values = SelectedValues.Split(_separator);
        foreach (string val in values)
        {
            List<OptionTreeNode> result = RecursiveFindNodesAndSubNodesById(Children, val);
            foreach (OptionTreeNode node in result)
            {
                selectedNodes.Add(node);
            }
        }
        return selectedNodes;
    }

    /// <summary>
    /// Recursively search through the first level in the Options Nodes in the OptionsTree looking the selected Id.
    /// </summary>
    /// <param name="node">The list that will be searched.</param>
    /// <param name="id">Id to search.</param>
    /// <returns>The Node with the indicated Id and its sub nodes.</returns>
    private List<OptionTreeNode> RecursiveFindNodesAndSubNodesById(List<OptionTreeNode> node, string id)
    {
        List<OptionTreeNode> result = new List<OptionTreeNode>();
        foreach (OptionTreeNode subNode in node)
        {
            if (subNode.Id == id)
            {
                if (subNode.Children != null && subNode.Children.Any())
                {
                    foreach (OptionTreeNode innerElement in subNode.Children)
                    {
                        result.Add(innerElement);
                        if (innerElement.Children != null && innerElement.Children.Any())
                            result.AddRange(AddRecursiveNodes(innerElement));
                    }
                }
                else
                {
                    result.Add(subNode);
                }
            }
            else if (subNode.Children != null && subNode.Children.Any())
            {
                List<OptionTreeNode> recursiveResult = RecursiveFindNodesAndSubNodesById(subNode.Children, id);
                if (recursiveResult.Any())
                    foreach (OptionTreeNode innerElement in recursiveResult)
                    {
                        result.Add(innerElement);
                    }
            }
        }
        return result;
    }

    public List<OptionTreeNode> AddRecursiveNodes(OptionTreeNode node)
    {
        List<OptionTreeNode> result = new List<OptionTreeNode>();
        foreach (OptionTreeNode subNode in node.Children)
        {
            result.Add(subNode);
            if (subNode.Children != null && subNode.Children.Any())
            {
                foreach (OptionTreeNode innerNode in subNode.Children)
                {
                    result.AddRange(AddRecursiveNodes(innerNode));
                }
            }

        }
        return result;
    }

    public class OptionTreeNode
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public bool IsDisabled { get; set; }
        public long ParentId { get; set; }
        public long EntityId { get; set; }
        public List<OptionTreeNode> Children { get; set; } = new List<OptionTreeNode>();
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoleCrudDto, RoleCrudModel>();
        }
    }
}
