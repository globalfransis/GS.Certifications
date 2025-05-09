$(function () {
    assignConfirmationEvent();
    insertDataTables();
    assignShowHidePassword();
    showMessages();
});


function showSpinner(loading) {

    let spinner = document.getElementById("spinner");
    let blur = document.getElementById("layoutbody");
    if (loading) {
        spinner.style.display = "";
        blur.classList.add('active');
    } else {
        spinner.style.display = "none";
        blur.classList.remove('active');

    }

    toggleDisabledElements(loading)

}


function toggleDisabledElements(disable) {
    // Obtenemos todos los elementos que tienen el atributo 'disabled' o sean inputs, selects, buttons, etc.
    var elements = document.querySelectorAll("[disabled], input, select, textarea, button, a");
    
    // Para cada elemento y según el parámetro 'disable':
    // 1. setteamos o removemos la clase 'deshabilitado'
    // 2. detenemos la propagación de los eventos click, keydown, tap y focus 
    elements.forEach(function(element) {
        if (disable) {
            element.classList.add('deshabilitado');
            element.addEventListener("click", detenerPropagacionEvento);
            element.addEventListener("keydown", detenerPropagacionEvento);
            element.addEventListener("tap", detenerPropagacionEvento);
            element.addEventListener("focus", detenerPropagacionEvento);
        } else {
            element.classList.remove('deshabilitado')
            element.removeEventListener("click", detenerPropagacionEvento);
            element.removeEventListener("keydown", detenerPropagacionEvento);
            element.removeEventListener("tap", detenerPropagacionEvento);
            element.removeEventListener("focus", detenerPropagacionEvento);
        }
    });
}

function detenerPropagacionEvento(evento) {
    evento.preventDefault();
    evento.stopPropagation();
}

function onLogout() {
    clearLocalStorage();
}

function onLogin() {
    clearLocalStorage();
}

function getUrl() {
    let loc = window.location.pathname;
    let dir = baseUrl + loc.substring(0, loc.lastIndexOf('/'));
    return dir;
}

function showMessages() {
    try {
        if (__errorMessage != null && __errorMessage != "") {
            showMessageError(__errorMessage);
        }
        if (__alertMessage != null && __alertMessage != "") {
            showMessageAlert(__alertMessage);
        }
        if (__successMessage != null && __successMessage != "") {
            showMessageSuccess(__successMessage);
        }
    } catch (error) {
        //console.log("Error mostrandno mensajes: " + error)
    }
}

// #region DataTables

/**
 * Renders dataTable to table elements with the 'convert-to-datatable' attribute.
 * All the the elements with the 'no-sort-datatable' attribute will be set to have 'orderable: false' to its column.
 * The 'no-paging' remove all the paging element of the table, like the combo for the ammount of records per page and the page buttons.
 */
function insertDataTables() {
    //Passing all parameters in one attribute -> Test='{ "Paging":[[10, 25, 50, -1], [10, 25, 50, "@loc["Todos"]"]], "autoWith": true}'
    //Example length-menu-datatable=@Json.Serialize("[[10, 25, 50, 100], [10, 25, 50, 100]]")
    $("table[convert-to-datatable]").each(function () {
        initializeDataTable($(this));
    });
}

function checkIfDataTableIsInitialized(id) {
    return $.fn.DataTable.isDataTable('#' + id);
}

/**
 * Does the same of the function insertDataTables(), but only to the table with the id specified. The other difference with insertDataTables is that this function will not be used on the document.ready, its activated manually.
 * @param {any} id
 */
function insertDataTablesManual(id) {
    var $element = $("table[convert-to-datatable-manual]#" + id);
    initializeDataTable($element);
}

/**
 * Destroy the specified datatable.
 * @param {any} id
 * @param {any} preserveState
 */
function destroyDataTablesManual(id, preserveState) {
    if (checkIfDataTableIsInitialized(id)) {
        var $element = $("table[convert-to-datatable-manual]#" + id).DataTable();
        if (undefined === undefined || preserveState !== true)
            $element.state.clear(); // Borrar el estado guardado

        $element.clear();
        $element.destroy();
    }
}

/**
 * Destroy the specified datatable without erasing the data.
 * @param {any} id
 * @param {any} preserveState
 */
function onlyDestroyDataTablesManual(id, preserveState) {
    if (checkIfDataTableIsInitialized(id)) {
        var $element = $("table[convert-to-datatable-manual]#" + id).DataTable();
        if (!preserveState)
            $element.state.clear();

        // Si la tabla usa paginación del lado del servidor, desactivamos el eventListener para order.dt
        // Esto es necesario para evitar que al volver a inicializar la datatable se vuelva a disparar 
        // el evento custom columnSorted, evitando que potencialmente se hagan 2 requests
        var serverSidePaging = $("table[convert-to-datatable-manual]#" + id).attr("server-side-paging");
        if (serverSidePaging != undefined) $("table[convert-to-datatable-manual]#" + id).off('order.dt');

        $element.destroy();
    }
}

function clearStateDataTable(id) {
    if (checkIfDataTableIsInitialized(id)) {
        var $element = $("table[convert-to-datatable-manual]#" + id).DataTable();
        $element.state.clear().draw(); // Borrar el estado guardado* /

        var $element = $("table[convert-to-datatable]#" + id).DataTable();
        $element.state.clear().draw(); // Borrar el estado guardado* /
    }
}

function initializeDataTable($element) {

    // We create a custom sort function for the DataTables date format "datetime-moment" (DD/MM/YYYY HH:mm).
    $.extend($.fn.dataTable.ext.type.order, {
        'datetime-moment-pre': function (a) {
            var date = a.replace(/[^0-9/:" ]/g, "") // remove anything that isn't a valid datetime char (not digit, '/', ':' or ' ') 
            var dateParts = date.length > 0 ? date.split(' ') : null;
            if (date.length > 0) { // if date specified (case date or datetime)
                var dateObject = moment(dateParts[0], 'DD/MM/YYYY');
                if (dateParts.length > 1) { // if time specified (case datetime)
                    var timeParts = dateParts[1].split(':');
                    dateObject.hours(timeParts[0]);
                    dateObject.minutes(timeParts[1]);
                    dateObject.seconds(timeParts[2]);
                    return dateObject.format('YYYYMMDDHHmmss');
                }
                return dateObject.format('YYYYMMDD')
            }
        return null // not a date nor datetime (for instance, received '-' or anything that represents null date)
        },
        'datetime-moment-asc': function (a, b) {
            return a < b ? -1 : a > b ? 1 : 0;
        },
        'datetime-moment-desc': function (a, b) {
            return a < b ? 1 : a > b ? -1 : 0;
        }
    });

    //False means that there is no data, so DataTables shouldnt be initialized.
    //Other options $element.children("tbody").children("tr").children("td").length > 1 -> (only one td to show the text 'No se encontraron datos')
    // $element.children("tbody").children("tr").attr('class') != 'no-data'
    if ($element.find('tr.no-data').length != 1) { //->this means that a td with the class 'no-data' dont exist.

        var ordering = $element.attr("no-ordering");
        var searching = $element.attr("no-search");
        var caseSensitive = $element.attr("case-sensitive");
        var noPaging = $element.attr("no-paging");
        var serverSidePaging = $element.attr("server-side-paging");
        var parsedLengthMenu = $element.attr("length-menu-datatable");
        var lengthChange = $element.attr("length-change")
        var lengthMenu = parsedLengthMenu ? JSON.parse(parsedLengthMenu) : null;
        var notOrderableColumns = [];
        var defaultOrderColumn = { index: null, order: null };
        $element.find("th[no-sort-datatable]").each(function () {
            notOrderableColumns.push($(this).index());
        });
        $element.find("th[default-sort-datatable]").each(function () {
            let elem = $(this).attr("default-sort-datatable")
            defaultOrderColumn.index = $(this).index();
            defaultOrderColumn.order = elem;
        });

        var aoColumns = [];
        var orderableColumnsByDatetime = []

        $element.find("th:not([no-datatable])").each(function () {
            let elem = $(this).attr("datatable-datetime")
            if (elem === undefined || elem === false || elem === null)
                aoColumns.push(null)
            else {
                orderableColumnsByDatetime.push($(this).index())
                aoColumns.push({ type: 'datetime-moment' })
            }
        });

        $element.DataTable({
            "stateSave": true,
            "info": false, //->hides info in the lower left corner.
            "autoWidth": false,
            "responsive": true,
            "paging": noPaging == undefined && serverSidePaging == undefined ? true : false,
            "columnDefs": [
                {
                    targets: orderableColumnsByDatetime,
                    type: 'datetime-moment',
                },
                { orderable: false, targets: notOrderableColumns }],
            //   "aoColumns": aoColumns,
            //"order": [], //Dont apply a default sort.
            "lengthMenu": lengthMenu ?? defaultLengthMenuOptDataTable,
            "pagingType": "simple_numbers", //"full_numbers",-> shows numbers, prev, next, first and last
            "autoWidth": false,
            "ordering": ordering == undefined ? true : false,
            "lengthChange": true,
            "language": languagesDataTable,
            "searching": searching == undefined ? true : false,
            "lengthChange": lengthChange == undefined ? true : false,
            "search": {
                "caseInsensitive": caseSensitive == undefined ? true : false,
            },
            "order": defaultOrderColumn.index == null && defaultOrderColumn.order == null ? [] : [[defaultOrderColumn.index, defaultOrderColumn.order]],
            //this overwrite the language.
            //"language": {
            //    "url": "https://cdn.datatables.net/plug-ins/1.10.22/i18n/Spanish.json"
            //}
        });

        // Si la paginación es del lado del servidor -> interceptamos el evento de ordenamiento de columnas para obtener
        // el nombre de la columna y la direccion de ordenamiento.
        // De este modo podemos utilizar estos datos como parámetros al manejar el evento custom 'columnSorted'
        if (serverSidePaging != undefined) {
            $element.on('order.dt', function orderDtHandler(e, settings) {
                e.stopImmediatePropagation();  // Bloquea la ejecución del handler original
                var order = $element.DataTable().order();
                
                // En algunos casos order puede estar vacía, recordemos que este evento `order.dt` se dispara
                // también en la inicialización de la tabla
                var columnIndex = order[0][0] != null && order[0][0] != undefined ? order[0][0] : defaultOrderColumn.index; // Obtenemos el índice de la columna ordenada
                var orderDirection = order[0][1] != null && order[0][1] != undefined ? order[0][1] : defaultOrderColumn.order; // Obtenemos la dirección de ordenamiento (asc o desc)

                // Obtenemos el nombre de la columna utilizando el índice
                // var columnName = $($element.DataTable().columns(columnIndex).header()).html();
                var columnName = $($element.DataTable().columns(columnIndex).header()).attr('data-column');

                if (!columnName) {
                    console.error("El `th` en la posición " + columnIndex + " no tiene un atributo `data-column`.");
                    throw new Error("Falta el atributo `data-column` en el `th` de la columna ordenada.");
                }
                
                var customEvent = new CustomEvent('columnSorted', {
                    detail: {
                        columnIndex: columnIndex,
                        columnName: columnName,
                        orderDirection: orderDirection
                    }
                });
                
                // Disparamos el evento personalizado en el elemento de la tabla
                e.target.dispatchEvent(customEvent);
                
            });
        }
    }
}

// #endregion

// #region Delete Button

/**
 * Assigns a window.confirm to the click event of all html elements with a 'confirm-delete' attribute, showing the text assigned to the attribute.
*/

var emitEvent = false;
function assignConfirmationEvent() {
    var buttons = $("[confirm-delete]");

    buttons.click(async function (event) {
        var text = this.getAttribute("confirm-delete")
        var id = this.getAttribute("id")
        await cancelOrEmitEvent(text, id)
    });
}

async function cancelOrEmitEvent(text, id) {
    var pButton = 'Aceptar';
    var sButton = 'Cancelar';
    if (!emitEvent) {
        event.preventDefault();
        if (await showModalForConfirmMessage(text, pButton, sButton)) {
            emitEvent = true;
            document.getElementById(id).click();
            showSpinner(true)
        }
        removeSpinners();
    }
}

function confirmActionModal(messageModal, messagePrimaryButton, messageSecondaryButton) {
    return showModalForConfirmMessage(messageModal, messagePrimaryButton, messageSecondaryButton);
}

function confirmEventManual(message) {
    var result = window.confirm(message);
    if (!result) {
        removeSpinners();
        //event.preventDefault();
    }

    return result;
}

// #endregion

// #region UI - various

/**
 * Change the icon and the type of the Inputs inside a div tag that have the 'input-password' attribute.
*/
function assignShowHidePassword() {
    var inputs = $("[input-password] button");

    inputs.click(function (event) {
        event.preventDefault();
        var input = $(this).siblings("input");
        if (input.attr("class").includes("form-control pw")) {
            $(this).children().removeClass("fas fa-eye-slash");
            $(this).children().addClass("fas fa-eye");
            input.removeClass("pw");
            input.prop('type', 'text');
        } else if (input.attr("class").includes("form-control")) {
            $(this).children().removeClass("fas fa-eye");
            $(this).children().addClass("fas fa-eye-slash");
            input.addClass("pw");
            input.prop('type', 'password');
        }
    });
}

/**
 * Click the button of the Main menu to collapse or uncollapse it.
 */
function clickMainMenu() {
    $("#ham-button").click();
}

/**
 * Force the closure of the OffCanvas open if there is any.
 */
function closeOffCanvasWithCloseButton() {
    //$(".offcanvas.show").removeClass("show");
    //var offCanvas = $(".offcanvas.show");
    //offCanvas.each((index, element) => {
    //    /*var id = element.id;*/
    //    var bsOffcanvas = new bootstrap.Offcanvas(element);
    //    bsOffcanvas.hide();
    //})
    $("button[data-bs-dismiss='offcanvas']").click();
}

// #endregion

// #region Modals

/**
 * Load a partialView in the div with the 'modal-section' attribute, then open the modal with the 'modal-partial' attribute.
 * Only one 'modal-section' attribute must exist to work properly.
 * Only one 'modal-partial' attribute must exist to work properly.
*/
function openModal(data) {
    $('[modal-section]').html(data);
    $('[modal-partial]').modal('show');
}

/**
 * Hides the specified modal.
 * @param {any} id modalId.
 */
function hideModelManual(id) {
    $("#" + id).modal('hide');
}

/**
 * Close the modal with the 'modal-partial' attribute.
*/
function successfulOperation() {
    $("[modal-partial]").click();
    location.reload();
}

/**
 * Sets callbacks to open and close events of the selected modal.
 * @param {any} modalElement
 * @param {any} openCallback
 * @param {any} closeCallback
 */
function subscribeCallbacksToModalEvents(modalElement, openCallback, closeCallback) {
    $(modalElement).on("show.bs.modal", openCallback);
    $(modalElement).on("hidden.bs.modal", closeCallback);
}

/**
 * Shows an alert.
 * @param {any} message Message to be shown in the alert.
 */
function handleModalLoadError(message) {
    alert(message.value);
}

// #endregion

// #region Activation and Deactivation of Controls

/**
 * Hide all the buttons and changes all inputs and selects to readonly.
 * Any extra element that are wanted to be hidden must have the 'hide-in-detail' attribute,
 * Those that are wanted to be shown must have the 'show-in-detail' attribute.
*/
function deactivateInputs() {
    $('.content form input, .content form select').attr('readonly', true);
    $('.content form textarea, .content form select').attr('readonly', true);
    $('.content form input[type=checkbox]').attr('readonly', true);
    $('.content form input[type=number]').attr('readonly', true);

    $('.content form input[type=checkbox]').addClass("checkbox-disabled");
    $("[includes-checkbox]").addClass("checkbox-disabled");


    $('.content form number').attr('disabled', true);
}

function deactivateControls() {
    $('.content form input, .content form select').attr('readonly', true);
    $('.content form textarea, .content form select').attr('readonly', true);
    $('.content form input[type=checkbox]').attr('readonly', true);

    $('.content form input[type=checkbox]').addClass("checkbox-disabled");
    $("[includes-checkbox]").addClass("checkbox-disabled");

    $('.content form select').attr('disabled', true);

    //$('.content form a').addClass("checkbox-disabled"); toDo: check
    //Avoid carat Browsing mode.
    //$('.content form input, .content form select').attr('disabled', true);
    //$('.content form textarea, .content form select').attr('disabled', true);
    //$('.content form input[type=checkbox]').attr('disabled', true);
    $('.content form button').hide();

    $("[show-in-detail]").show();
    $("[hide-in-detail]").attr('style', 'display: none !important');
}

function deactivateAllControls() {
    $('.content input, .content select').attr('readonly', true);
    $('.content textarea, .content select').attr('readonly', true);
    $('.content input[type=checkbox]').attr('readonly', true);
    $('.content input[type=checkbox]').addClass("checkbox-disabled");
    $("[includes-checkbox]").addClass("checkbox-disabled");
    $('.content select').attr('disabled', true);
    $('.content button').hide();
    $("[show-in-detail]").show();
    $("[hide-in-detail]").attr('style', 'display: none !important');
}

/**
 * Show all the buttons and changes from all inputs and selects the readonly property to false.
 * Any extra element hidden by 'hide-in-detail' attribute will be shown again.
*/
function reactivateControls() {
    $('.content form input, .content form select').attr('readonly', false);
    $('.content form textarea, .content form select').attr('readonly', false);
    $('.content form input[type=checkbox]').attr('readonly', false);

    $('.content form input[type=checkbox]').removeClass("checkbox-disabled");
    $("[includes-checkbox]").removeClass("checkbox-disabled");

    $('.content form a').removeClass("checkbox-disabled");

    $('.content form select').attr('disabled', false);
    //Avoid carat Browsing mode.
    //$('.content form input, .content form select').attr('disabled', true);
    //$('.content form textarea, .content form select').attr('disabled', true);
    //$('.content form input[type=checkbox]').attr('disabled', true);
    $('.content form button').show();

    $("[disabled-while-loading]").show();
    $("[disabled-while-loading]").attr('disabled', false);

    //$("[show-in-detail]").show();
    $("[hide-in-detail]").attr('style', 'display: unset !important');
}

/**
 * IMPORTANT: To be used while the data is being recovered in a Vue component or via Javascript AJAX call.
 * Hide all the buttons and changes all inputs y selects to readonly.
 * Any extra element that are wanted to be hidden must have the 'hide-in-detail' attribute,
 * Those with the 'show-in-detail' attribute will still be hidden, must be changed after loading the data of the component.
*/
function deactivateControlsWhileLoading() {
    $('.content form input, .content form select').attr('readonly', true);
    $('.content form textarea, .content form select').attr('readonly', true);
    $('.content form input[type=checkbox]').attr('readonly', true);

    $('.content form input[type=checkbox]').addClass("checkbox-disabled");
    $("[includes-checkbox]").addClass("checkbox-disabled");

    $('.content form a').addClass("checkbox-disabled");

    $('.content form select').attr('disabled', true);
    //Avoid carat Browsing mode.
    //$('.content form input, .content form select').attr('disabled', true);
    //$('.content form textarea, .content form select').attr('disabled', true);
    //$('.content form input[type=checkbox]').attr('disabled', true);
    $('.content form button').hide();

    $("[disabled-while-loading]").show();
    $("[disabled-while-loading]").attr('disabled', true);

    $("[show-in-detail]").hide();
    $("[hide-in-detail]").attr('style', 'display: none !important');
}

/**
 * IMPORTANT: To be used after the data is has been recovered in a Vue component or via Javascript AJAX call.
 * Show the elements with the 'show-in-detail' attribute.
 */
function showAfterLoading() {
    $("[show-in-detail]").show();
}

// #endregion

// #region Use of Controls

function preventUpAndDownArrowKeys(event) {
    var result = false;

    var arrowUp = 38;
    var arrowDown = 40;
    var code = (event.keyCode ? event.keyCode : event.which);
    if (code == arrowUp || code == arrowDown) {
        event.preventDefault();
        result = true;
    }

    return result;
}

/**
 * Prevent that a negative symbol can be inserted in an input.
 * @param { Event } event Javascript Event.
*/
function preventNegativeNumbers(event) {
    var dotKey = 190;
    var slashKeyCode = 189;
    var commaKey = 188;
    var plusKey = 187;
    var dotNumPadKey = 110;
    var minusSignKeyCode = 109;
    var plusNumPadKey = 107;
    var code = (event.keyCode ? event.keyCode : event.which);
    if (code == dotKey || code == slashKeyCode || code == commaKey || code == plusKey || code == dotNumPadKey || code == minusSignKeyCode || code == plusNumPadKey)
        event.preventDefault();
};

/**
 * Only accepts numbers from 0 to 9. If its permitted the funtions returns true. If not preventDefault.
 * @param { Event } event Javascript Event.
 */
function admitOnyIntegersAndPositiveNumbers(event) {
    var code = (event.keyCode ? event.keyCode : event.which);

    if (code >= 37 && code <= 40) return true;  // arrows
    if (code >= 48 && code <= 57) return true; // 0 al 9 (traditional)
    if (code >= 96 && code <= 105) return true; // 0 al 9 (keypad)   
    if (code >= 1112 && code <= 123) return true; // F1 a F12

    if (code == 8) return true; // backspace
    if (code == 46) return true; // delete

    if (code == 9) return true; // tab
    if (code == 35) return true; // end
    if (code == 36) return true; // home
    event.preventDefault();
}

/**
 * Find the input in the row and checked as true. Works with radioButtons, not with checkbox.
 * @param { Event } event Javascript Event.
*/
function makeRowSelectableRadioButtons(event) {
    //$('[selectable-row] tr').click(function () {
    //    $(this).children('td').children('input').prop('checked', true);

    //    // To change the background color of the selected row - neeeds a new css class.
    //    //$('[selectable-row] tr').removeClass('selected');
    //    //$(this).toggleClass('selected');
    //});
    $(event.target).siblings('td').children('input').prop('checked', true);
};

/**
 * Find the input in the row and checked as true. Works with radioButtons, not with checkbox.
 * @param { Event } event Javascript Event.
 * @param { bool } checkValue Indicates if its checked or not.
*/
function makeRowSelectableCheckBox(event, checkValue) {
    //Avoid comparing if the target is the input itself.
    if (!$(event.target).is("input")) {
        if ($(event.target).siblings('td').children('input').prop('checked') != checkValue)
            $(event.target).siblings('td').children('input').prop('checked', checkValue);
    }
};

// #endregion

// #region Others

/**
 * For the partialViews shows the summary of the errors in the div with the 'ajax-errors' attribute.
*/
function handleAjaxCallError(error) {
    var errorDetails = error.responseJSON.errors;
    var message = "";
    if (errorDetails != null) {
        errorDetails.Model.forEach(element => {
            message = message + "<span style='display: inline-block'>" + element + "</span>";
        })
    }
    else {
        message = error.responseJSON.message.value ? error.responseJSON.message.value : error.responseJSON.message;
        message = "<span>" + message + "</span>";
    }
    $("[ajax-errors]").html(message);
}

/**
 * Changes the Url visible to the User to hide values like 'handlers', etc.
 * @param {any} mode Value used to set in the Url.
 * @param {any} pageTitle Value to be set in the 'Title' value of window.history.
 * @param {any} backUrl Unused to the moment.
 */
function replaceWindowsHistory(mode, pageTitle, backUrl) {
    var modeDirection = "";

    switch (mode) {
        case 'Detalle':
            modeDirection = 'Detail';
            break;
        case 'Modificación', 'Modificaci&#xF3;n':
            modeDirection = 'Edit';
            break;
        case "Alta":
            modeDirection = 'Create';
    };

    if (mode != 'Detalle') {
        var indexHandler = window.location.href.indexOf('handler');
        if (indexHandler == -1)
            modeDirection = window.location.href;
        window.history.replaceState(null, pageTitle, modeDirection);
    }

    //var elements = $("[replaceWindowsHistory]");

    //elements.click(function () {
    //    window.history.replaceState(null, pageTitle, backUrl)
    //});
}

/**
     * Does the same of the function insertDataTables(), but only to the table with the id specified. The other difference with insertDataTables is that this function will not be used on the document.ready, its activated manually.
     * @param {any} id
     */
function focusOnError() {
    var elements = $(".field-validation-error")
    var i = 0
    var h = true
    var element = null

    while (i < elements.length && h) {
        if (elements[i].innerText.replace(/(?:\r\n|\r|\n)/g, '').replace(/\s/g, '') != '') {
            element = elements[i];
            h = false;
        }
        i++;
    }
    if (element) {
        $(element).parent()[0].scrollIntoView();
    } else {
        if ($("input.invalid").length > 0)
            $("input.invalid")[0].scrollIntoView();
    }
}

/**
 * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
 * @param {any} title
 * @param {any} message
 */
function showSimpleToastWithTitle(title, message) {
    $("#layoutToast").html(
        //"<div class=\"position-absolute p-3 top-50 start-50 translate-middle\">\n" +
        "<div class=\"position-fixed bottom-0 end-0 p-3 mb-4\" style=\"z-index: 2000\">\n" +
        "<div id =\"dinamicToastWithTitle\" class=\"toast hide\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\n" +
        "<div class=\"toast-header modalAndToastHeader\">\n" +
        "<strong class=\"me-auto\">" + title + "</strong>\n" +
        /*"<small class=\"text-muted\">11 mins ago</small>\n" +*/
        "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"toast\" aria-label=\"Close\"></button>\n" +
        "</div>\n" +
        "<div class=\"toast-body\">\n" +
        message +
        "</div>\n" +
        "</div>\n" +
        "</div>\n");

    $("#dinamicToastWithTitle").toast('show');
}

/**
 * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
 * @param {any} title
 * @param {any} message
 */
function showExclamationToast(message, type) {
    $("#layoutToast").html(
        //"<div class=\"position-absolute p-3 top-50 start-50 translate-middle\">\n" +
        "<div class=\"position-fixed bottom-0 end-0 p-3 mb-4\" style=\"z-index: 2000\">\n" +
        "<div id =\"dinamicToastWithTitle\" class=\"toast hide\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\n" +
        "<div class=\"toast-header modalAndToastHeader" + type + "\">\n" +
        "<strong class=\"me-auto\"></strong>\n" +
        /*"<small class=\"text-muted\">11 mins ago</small>\n" +*/
        "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"toast\" aria-label=\"Close\"></button>\n" +
        "</div>\n" +
        "<div class=\"toast-body\">\n" +
        message +
        "</div>\n" +
        "</div>\n" +
        "</div>\n");



    $("#dinamicToastWithTitle").toast('show')

}

/**
 * Display a Toast in the low left corner with only a message. The Toast will be dismissed automatically.
 * @param {any} message
 */
function showSimpleToastWithoutTitle(message) {
    $("#layoutToast").html(
        "<div class=\"position-fixed bottom-0 end-0 p-3 mb-4\" style=\"z-index: 2000\">\n" +
        "<div id =\"dinamicToastWithoutTitle\" class=\"toast align-items-center hide\" role=\"alert\" aria-live=\"assertive\" aria-atomic=\"true\">\n" +
        "<div class=\"d-flex\">" +
        "<div class=\"toast-body\">\n" +
        message +
        "</div>\n" +
        "<button type=\"button\" class=\"btn-close me-2 m-auto\" data-bs-dismiss=\"toast\" aria-label=\"Close\"></button>\n" +
        "</div>\n" +
        "</div>\n" +
        "</div>\n");

    $("#dinamicToastWithoutTitle").toast('show');
}

/**
 * Shows a modal with a message, it doesnt have any buttons.
 * @param {any} message
 */
function showModalForErrorMessage(message) {
    $("#layoutModalContents").remove();
    $("#layoutModalContents").empty();
    $("#layoutModal").removeAttr("data-bs-backdrop", "static");
    $("#layoutModal").html(
        "<div id=\"layoutModalContents\" class=\"modal-dialog modal-dialog-centered\">\n" +
        //"<div class=\"modal-content\">\n" + //If the title is removed.
        "<div class=\"modal-content border-0\">\n" +
        "<div class=\"modal-header modalAndToastHeader py-2\">\n" +
        "<h5 class=\"modal-title\">SCX</h5>\n" +
        "<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"Close\"></button>\n" +
        "</div>\n" +
        "<div class=\"modal-body\">\n" +
        "<p class=\"mb-0\">" + message + "\</p>\n" +
        "</div>\n" +
        "</div>\n" +
        "</div>\n");
    $("#layoutModal").modal('show');
}

/**
 * Shows a modal with a message and buttons to confirm. Calls a deferred and returns a boolean wether the user confirms or not.
 * @param {any} messageModal
 * @param {any} messagePrimaryButton
 * @param {any} messageSecondaryButton
 */
async function showModalForConfirmMessage(messageModal, messagePrimaryButton, messageSecondaryButton) {
    var result = false;
    await showModalForConfirmMessagePromise(messageModal, messagePrimaryButton, messageSecondaryButton)
        .then(f => { result = true; })
        .catch(f => { result = false; });

    return result;
}

/**
 * Shows a modal with a message and buttons to confirm. Returns a Promise wether the user accepts o reject the action.
 * The modal is static, it doesnt dissapear if the user click outside the modal.
 * @param {any} messageModal
 * @param {any} messagePrimaryButton
 * @param {any} messageSecondaryButton
 */
function showModalForConfirmMessagePromise(messageModal, messagePrimaryButton, messageSecondaryButton) {
    $("#layoutModalContents").remove();
    $("#layoutModalContents").empty();
    $("#layoutModal").attr("data-bs-backdrop", "static");
    $("#layoutModal").html(
        "<div id=\"layoutModalContents\" class=\"modal-dialog modal-dialog-centered\">\n" +
        "<div class=\"modal-content border-0\">\n" +
        "<div class=\"modal-header modalAndToastHeader py-2\">\n" +
        "<h5 class=\"modal-title\">"+ appName +"</h5>\n" +
        "<button id=\"layoutModalCloseButton\" type=\"button\" class=\"btn-close\" aria-label=\"Close\"></button>\n" +
        "</div>\n" +
        "<div class=\"modal-body\">\n" +
        "<p class=\"mb-0\">" + messageModal + "\</p>\n" +
        "</div>\n" +
        "<div class=\"modal-footer py-2\">\n" +
        "<button id=\"layoutModalPrimaryButton\" type=\"button\" class=\"btn btn-primary btn-sm\">" + messagePrimaryButton + "\</button>\n" +
        (messageSecondaryButton ? "<button id=\"layoutModalSecondaryButton\" type=\"button\" class=\"btn btn-secondary btn-sm\">" + messageSecondaryButton + "\</button>\n" : "") +
        "</div>\n" +
        "</div>\n" +
        "</div>\n");

    $("#layoutModal").modal('show');
    return new Promise(function (resolve, reject) {
        $("#layoutModalPrimaryButton").click(function () {
            hideModelManual("layoutModal");
            $("#layoutModal").on('hidden.bs.modal', function (event) {
                resolve();
            });
        });
        $("#layoutModalSecondaryButton").click(function () {
            hideModelManual("layoutModal");
            $("#layoutModal").on('hidden.bs.modal', function (event) {
                reject();
            });
        });
        $("#layoutModalCloseButton").click(function () {
            hideModelManual("layoutModal");
            $("#layoutModal").on('hidden.bs.modal', function (event) {
                reject();
            });
        });
    });
}

// #endregion


// #region Messages and Errors 

/**
 * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
 * @param {any} message
 */
function showMessageErrorAndFocus(message) {
    showExclamationToast(message, "Error")
    focusOnError()
}

/**
 * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
 * @param {any} message
 */
function showMessageError(message) {
    showExclamationToast(message, "Error")
}

/**
 * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
 * @param {any} message
 */
function showMessageSuccess(message) {
    showExclamationToast(message, "Success")
}

/**
 * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
 * @param {any} message
 */
function showMessageAlert(message) {
    showExclamationToast(message, "Alert")
}

/**
 * Display a system error as title.
 * @param {any} errormessage
 */
function showMessageMainError(errormessage) {
    clearMessageMainError();
    document.getElementById('clientPersistentMessage').innerHTML +=
        `<div class="text-center mt-4"><div class="col"><div class="fw-bold alert alert-danger font-weight-bold">${errormessage}</div></div></div >`;
}

/**
 * Display a system error as title and lock UI.
 * @param {any} errormessage
 */
function showMessageMainErrorAndLock(errormessage) {
    clearMessageMainError();
    deactivateAllControls();
    document.getElementById('clientPersistentMessage').innerHTML +=
        `<div class="text-center mt-4"><div class="col"><div class="fw-bold alert alert-danger font-weight-bold">${errormessage}</div></div></div >`;
}

/**
 * Clear any system error showed.
 */
function clearMessageMainError() {
    document.getElementById('clientPersistentMessage').innerHTML = ''
}

/*
 * Grays out element by id if grayOut is
 * true, else whites out element
 */
function grayOutElement(id, grayOut) {
    let element = document.getElementById(id)
    if (grayOut) {
        element.style.pointerEvents = "none";
        element.style.opacity = 0.5;
    } else {
        element.style.pointerEvents = "auto";
        element.style.opacity = 1;
    }
}

/*
 * Recibe un id de un elemento que contiene
 * un tooltip y lo muestra por 4 segundos
 */
function mostrarTooltip(tooltipId) {
    $(`#${tooltipId}`).tooltip("show");
    setTimeout(() => {
        $(`#${tooltipId}`).tooltip("hide");
    }, 4000);
}

/*
 *
 * This function must recive an argument ´unsafehtml´ 
 * that is a string of text containing encoded html content
 * 
 * The function will return a string of text reprenting 
 * the decoded html content
 */
function unescapeHtml(unsafeHtml) {

    return new DOMParser().parseFromString(unsafeHtml, "text/html").documentElement.textContent;

}
// #endregion



/*
 * Esta función devuelve true si la pantalla 
 * actual está en una resolucion mobile mediante
 * una expresion regular evaluada sobre el User-Agent
 * del browser
 */
function isMobile() {
    let check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true; })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
}

// function parsearConMiles(value) {
            
//     var exp = /\B(?=(\d{3})+(?!\d))/g;
//     const rep = '.';
    
//     if( value < 1000 && value > -1000){
//         return value;
//     }
//     return value.toString().replace(exp,rep);
// }

function ValidateEmail(mail) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) return true;
    return false;
}