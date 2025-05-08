$(function () {


    var currentOptionId = window.sessionStorage.getItem("currentOptionId");
    var parentId = window.sessionStorage.getItem("currentOptionParentId");
    var bigMenu = $(".main-side-menu #big-drawer");
    var slimMenu = $(".main-side-menu #slim-drawer");

    if (currentOptionId != null) {

        // resaltamos la opcion padre en la que estamos parados en el menu colapsado
        slimMenu.find(`[optionId="${parentId}"]`).addClass('active');
        slimMenu.find(`[optionId="${parentId}"]`).children().children().addClass('active');
        slimMenu.find(`[optionId="${parentId}"]`).addClass('menu-active');
        slimMenu.find(`[optionId="${parentId}"]`).children().children().addClass('menu-active');

        // home option
        if (currentOptionId == 0) {
            bigMenu.find("div.home-button").addClass('active');
            bigMenu.find("div.home-button").addClass('menu-active');
        } else {

            var $option = bigMenu.find(".list-group-item[optionid='" + currentOptionId + "']");

            //Lo comentamos porque el default pasa a ser que este el side menu colapsado
            $option.parents(".accordion-item").find(".collapsable-item").each(
                function () {
                    $(this).removeClass("collapsed");
                }
            );

            $option.parents(".accordion-collapse").each(
                function () {
                    $(this).addClass('show');
                }
            );

            $option.addClass('active');
            $option.addClass('menu-active');
            $option.parent().parent().find("div").addClass('show');

        }

    }

});

function RedirectToOption(option_id, parent_id, url) {

    //if (SetCurrentOption(option_id) == true) {
    //}
    //SetCurrentOption(option_id);

    window.sessionStorage.setItem("currentOptionId", option_id);
    window.sessionStorage.setItem("currentOptionParentId", parent_id);
    document.location.replace(url);

}

function SetCurrentOption(option_id) {

    var command = {
        optionId: option_id
    }

    $.ajax({
        url: '/api/options',
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: "json",
        data: JSON.stringify(command)
    })

}

$(function () {
    $("#backToMainMenu").click(function () {
        $("#mainMenu").show();
        $("#contextMenu").hide();
    });
    $("#goToContextMenu").click(function () {
        $("#mainMenu").hide();
        $("#contextMenu").show();
    });
});



