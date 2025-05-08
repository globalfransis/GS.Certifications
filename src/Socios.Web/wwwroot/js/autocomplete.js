//////////////////////////////////////////////////////////////////////////////////////////
// Deault Configuration
//////////////////////////////////////////////////////////////////////////////////////////

// Base API URL.
var ENDPOINT = ""; //default

// Relative URL used to search by value property.
var VALUE_QUERY = "/"; // default

// Relative URL used to seach by display property.
var FILTER_QUERY = "/filterByName/"; // default

// Number of elements showed when filter.
var TOP = 10; // default

// JSON property name used to bind the autocomplete.
var VALUE_PROPERTY = 'id' // default

// JSON property name showed to user.
var DISPLAY_PROPERTY = 'description'; // default 

// Initial input place holder.
var PLACEHOLDER = "search ..."; // default

// Characters required to execute the filter.
var THRESHOLD = 3; //default

//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Example markup:
//
//      <div class="col-6">
//          <label for="SourceCountryId" class="form-label">Pa&iacute;s de Origen</label>
//          <input id="SOURCE_COUNTRY" type="hidden" gsf-autocomplete name="SourceCountryId" value=""
//              displayProperty="name" valueProperty="alpha2Code" endpoint="https://restcountries.eu/rest/v2"
//              threshold="2" top="3" valueQuery="/alpha/" filterQuery="/name/" />
//      </div>
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

function getByValue(input, value) {

    var hidden = input.previousSibling;
    var endpoint = (hidden.getAttribute("endpoint") ?? ENDPOINT);
    var value_query = (hidden.getAttribute("valueQuery") ?? VALUE_QUERY);

    var queryUrl = endpoint + value_query + value;

    fetch(queryUrl)
        .then(response => response.json())
        .then(data => {

            var valueProperty = (hidden.getAttribute("valueProperty") ?? VALUE_PROPERTY);
            var displayProperty = (hidden.getAttribute("displayProperty") ?? DISPLAY_PROPERTY);
            var value = data[valueProperty];
            var name = data[displayProperty];

            SetSelected(input, value, name);
        })
        .catch(error => {
            console.log(error);
        });

}

function filterElements(input) {

    var hidden = input.previousSibling;
    var endpoint = (hidden.getAttribute("endpoint") ?? ENDPOINT);
    var filter_query = (hidden.getAttribute("filterQuery") ?? FILTER_QUERY);

    var queryUrl = endpoint + filter_query + input.value;

    fetch(queryUrl)
        .then(response => response.json())
        .then(data => {
            showResults(input, data);
        })
        .catch(error => {
            console.log(error);
        });

}

function SetSelected(input, value, name) {

    var hidden = input.previousSibling;
    var button = input.nextSibling;

    if (value == null) {
        hidden.value = '';
        input.value = '';
        input.removeAttribute("disabled");
        //this.style.visibility = 'hidden';
        button.style.display = 'none';
        input.focus();
    }
    else {
        hidden.value = value;
        input.value = (name ?? '');

        input.setAttribute("disabled", "disabled");

        //button.style.visibility = '';
        button.style.display = 'inline';

    }

}

var fnOnClearSelection = function (e) {
    SetSelected(this.previousSibling, null, null);
}

var fnOnInputBlur = function (e) {
    var resultTable = this.nextSibling.nextSibling;
    if (resultTable != null) resultTable.remove();
}

var fnOnResultClick = function (e) {

    //e.preventDefault()

    //////////////////////////////////////////////////////
    // get elements
    var resultTable = this.parentNode.parentNode.parentNode; // td parent (tr) parent (tbody) parent (table)
    var input = resultTable.previousSibling.previousSibling;
    //////////////////////////////////////////////////////

    SetSelected(input, this.previousSibling.innerHTML, this.innerHTML);

    resultTable.remove();

}

var fnOnInputKeyUp = function (e) {

    if (e.code == 'ArrowDown') return;
    if (e.code == 'ArrowUp') return;

    if (e.code == 'Escape') {
        var resultTable = this.nextSibling.nextSibling;
        if (resultTable != null) resultTable.remove();
        return;
    }

    input_val = this.value; // updates the variable on each ocurrence

    var hidden = this.previousSibling;
    threshold = (hidden.getAttribute("threshold") ?? THRESHOLD);

    if (input_val.length >= threshold)
        filterElements(this);
    else
        showResults(this, []);

}

var fnOnInputKeyDown = function (e) {

    var resultTable = this.nextSibling.nextSibling;

    switch (e.code) {
        case 'ArrowDown':
            changeHighlighted(resultTable, 'down');
            return false; // prevent onkeypressup
            break;

        case 'ArrowUp':
            changeHighlighted(resultTable, 'up');
            return false; // prevent onkeypressup
            break;

        case "Enter":
            selectHighlighted(resultTable);
            break;

        default:
            break;
    }

}

function selectHighlighted(resultTable) {

    for (var i = 0, row; row = resultTable.rows[i]; i++) {
        if (row.className == 'highlighted') {
            var input = resultTable.previousSibling.previousSibling;

            var value = row.cells[0].innerHTML;
            var name = row.cells[1].innerHTML;

            SetSelected(input, value, name);

            resultTable.remove();
        }
    }
}

function clearHighlighted(resultTable) {
    for (var i = 0, row; row = resultTable.rows[i]; i++) {
        row.className = "";
    }
}

function changeHighlighted(resultTable, direction) {

    var rowSelected = -1;

    for (var i = 0, row; row = resultTable.rows[i]; i++) {
        if (row.className == 'highlighted') rowSelected = i;
    }

    if (direction == 'down') {
        if (rowSelected == -1)
            resultTable.rows[0].className = 'highlighted';
        else {
            if (rowSelected < (resultTable.rows.length - 1)) {
                resultTable.rows[rowSelected].className = '';
                resultTable.rows[rowSelected + 1].className = 'highlighted';
            }
        }
    }

    if (direction == 'up') {
        if (rowSelected == -1)
            resultTable.rows[resultTable.rows.length - 1].className = 'highlighted';
        else {
            if (rowSelected > 0) {
                resultTable.rows[rowSelected].className = '';
                resultTable.rows[rowSelected - 1].className = 'highlighted';
            }
        }
    }

}


function showResults(input, results) {

    if (results.length > 0) {

        var resultTable = input.nextSibling.nextSibling;
        if (resultTable != null) resultTable.remove();

        var hidden = input.previousSibling;
        var displayProperty = (hidden.getAttribute("displayProperty") ?? DISPLAY_PROPERTY);
        var valueProperty = (hidden.getAttribute("valueProperty") ?? VALUE_PROPERTY);

        //////////////////////////////////////////////////////////
        // append  result element
        resultTable = document.createElement('table');
        resultTable.setAttribute("class", "table table-light table-bordered shadow");
        input.parentNode.insertBefore(resultTable, input.nextSibling.nextSibling);

        var tblBody = document.createElement("tbody");

        //////////////////////////////////////////////////////////

        var top = (hidden.getAttribute("top") ?? TOP);

        for (i = 0; i < results.length && i < top; i++) {

            var row = document.createElement("tr");

            var cell = document.createElement('td');
            cell.style.display = 'none';
            cell.innerHTML = results[i][valueProperty];
            row.appendChild(cell);

            var cell = document.createElement('td');
            cell.className = "display";
            cell.innerHTML = results[i][displayProperty];

            row.onmouseover = function (e) { this.className = 'highlighted'; }
            row.onmouseleave = function (e) { clearHighlighted(resultTable); }

            // 'mousedown' fires before 'click' event
            // so we can prevent 'blur' input event  fires.
            cell.addEventListener("mousedown", fnOnResultClick);

            row.appendChild(cell);

            tblBody.appendChild(row);

        }

        resultTable.appendChild(tblBody);
    }

}

document.addEventListener("DOMContentLoaded", function () {

    console.log('Preparando markup ...');

    // looking for autocomplete controls
    var autocompletes = document.querySelectorAll('input[type="hidden"][gsf-autocomplete]');

    //console.log(inputs);

    // for each autocomplete
    for (i = 0; i < autocompletes.length; i++) {

        var hidden = autocompletes[i];
        var placeholder = (hidden.getAttribute("placeholder") ?? PLACEHOLDER);


        hidden.removeAttribute("gsf-autocomplete");

        //////////////////////////////////////////////////////////
        // create wrapper container
        var wrapper = document.createElement('div');
        wrapper.classList.value = "gsf-autocomplete input-group";
        // insert wrapper before el in the DOM tree
        hidden.parentNode.insertBefore(wrapper, hidden);
        // move el into wrapper
        wrapper.appendChild(hidden);
        //////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////
        // append input textbox for search
        var inputEl = document.createElement('input');
        inputEl.setAttribute('type', 'text');
        inputEl.setAttribute('value', '');
        inputEl.setAttribute('placeholder', placeholder);
        inputEl.setAttribute('class', 'form-control');
        inputEl.setAttribute('autofocus', 'true');

        inputEl.onblur = fnOnInputBlur;
        inputEl.onkeydown = fnOnInputKeyDown;
        inputEl.onkeyup = fnOnInputKeyUp;


        wrapper.appendChild(inputEl);
        //////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////
        //append button for clear selection
        var buttonEl = document.createElement('a');
        buttonEl.setAttribute('class', 'btn btn-secondary');

        var icon = document.createElement("i");
        icon.className = "fas fa-times";
        buttonEl.appendChild(icon);

        //buttonEl.style.visibility = 'hidden';
        buttonEl.style.display = 'none';
        buttonEl.addEventListener("click", fnOnClearSelection);

        wrapper.appendChild(buttonEl);
        //////////////////////////////////////////////////////////    

        if (hidden.value == '') {
            SetSelected(inputEl, null, null);
        }
        else {
            getByValue(inputEl, hidden.value);
        }

    }

});

function SetAutocomplete(id, value) {
    var hidden = document.getElementById(id);

    if (hidden == null) {
        console.log('No se encontro el autocompletar con id = ' + id)
        return;
    }

    var input = hidden.nextElementSibling;
    getByValue(input, value)

}
