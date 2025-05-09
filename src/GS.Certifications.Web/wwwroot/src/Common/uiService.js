export default class UiService {
    constructor() {}

    // #region Datatables

    /**
     * Does the same of the function insertDataTables(), but only to the table with the id specified. The other difference with insertDataTables is that this function will not be used on the document.ready, its activated manually.
     * @param {any} id
     */
    transformToDataTablesManual(id) {
        insertDataTablesManual(id)
    }

    /**
     * Destroy the specified datatable.
     * @param {any} id
     * @param {any} preservaState
     */
    eraseDataTablesManual(id, preservaState) {
        destroyDataTablesManual(id, preservaState );
    }

    /**
    * Destroy the specified datatable without erasing the data.
    * @param {any} id
    * @param {any} preservaState
    */
    onlyDestroyDataTablesManual(id, preservaState) {
        onlyDestroyDataTablesManual(id, preservaState);
    }

    /**
    * Clean the specified datatable state without erasing the data.
    * @param {any} id
    */
    clearStateDataTable(id) {
        clearStateDataTable(id);
    }

    // #endregion

    // #region Confirm Button

    /**
     * Deprecated - Do not user.
     * @deprecated
     * @param {any} message
     */
    confirmButtonClick(message) {
        return confirmEventManual(message);
    }

    /**
     * Shows a modal with a message and buttons to confirm. Returns a boolean wether the user accepts o reject the action.
     * The modal is static, it doesnt dissapear if the user click outside the modal.
     * @param {any} messageModal
     * @param {any} messagePrimaryButton
     * @param {any} messageSecondaryButton
     */
    async confirmActionModal(messageModal, messagePrimaryButton, messageSecondaryButton) {
        return await showModalForConfirmMessage(messageModal, messagePrimaryButton, messageSecondaryButton);
    }

    /**
     * Shows a modal with a message and buttons to confirm. Returns a Promise wether the user accepts o reject the action.
     * The modal is static, it doesnt dissapear if the user click outside the modal.
     * @param {any} messageModal
     * @param {any} messagePrimaryButton
     * @param {any} messageSecondaryButton
     */
    async showActionModalPromise(messageModal, messagePrimaryButton, messageSecondaryButton) {
        return await showModalForConfirmMessagePromise(messageModal, messagePrimaryButton, messageSecondaryButton);
    }

    // #endregion

    // #region Messages and Errors     

    /**
     * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
     * @param {any} message
     */
     showMessageErrorAndFocus(message) {
        showMessageErrorAndFocus(message);
    }

    /**
     * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
     * @param {any} message
     */
    showMessageError(message) {
        showMessageError(message);
    }

    /**
     * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
     * @param {any} message
     */
    showMessageSuccess(message) {
        showMessageSuccess(message);
    }

    /**
     * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
     * @param {any} message
     */
    showMessageAlert(message) {
        showMessageAlert(message);
    }

    /**
     * Display a system error as title.
     * @param {any} errormessage
     */
     showMessageMainError(errormessage) {
        showMessageMainError(errormessage);
    }

    /**
     * Display a system error as title and lock UI.
     * @param {any} errormessage
     */
    showMessageMainErrorAndLock(errormessage) {
        showMessageMainErrorAndLock(errormessage);
    }

    /**
     * Clear any system error showed.
     */   
    clearMessageMainError() {
        clearMessageMainError();
    }

    // #endregion


    // #region Modals and Toast

    /**
     * Hides the specified modal.
     * @param {any} id modalId.
     */
    closeModalManual(id) {
        hideModelManual(id);
    }

    /**
     * Sets callbacks to open and close events of the selected modal.
     * @param {any} modalElement
     * @param {any} openCallback
     * @param {any} closeCallback
     */
    setModalEventsCallbacks(modalElement, openCallback, closeCallback) {
        subscribeCallbacksToModalEvents(modalElement, openCallback, closeCallback);
    }    

    /**
     * Display a Toast in the low left corner with a title and a message. The Toast will be dismissed automatically.
     * @param {any} title
     * @param {any} message
     */
    setAndShowToastWithTitle(title, message) {
        showSimpleToastWithTitle(title, message)
    }

    /**
     * Display a Toast in the low left corner with only a message. The Toast will be dismissed automatically.
     * @param {any} message
     */
    setAndShowToastWithoutTitle(message) {
        showSimpleToastWithoutTitle(message)
    }

    // #endregion

    // #region Activation and Deactivation of Controls

    /**
     * IMPORTANT: To be used while the data is being recovered in a Vue component or via Javascript AJAX call.
     * Hide all the buttons and changes all inputs y selects to readonly.
     * Any extra element that are wanted to be hidden must have the 'hide-in-detail' attribute,
     * Those with the 'show-in-detail' attribute will still be hidden, must be changed after loading the data of the component.
     */
    lockInterfaceWhileLoading() {
        deactivateControlsWhileLoading();
    }

    /**
     * Hide all the buttons and changes all inputs and selects to readonly.
     * Any extra element that are wanted to be hidden must have the 'hide-in-detail' attribute,
     * Those that are wanted to be shown must have the 'show-in-detail' attribute.
     */
    lockInterface() {
        deactivateControls();
    }

    /**
     * Show all the buttons and changes from all inputs and selects the readonly property to false.
     * Any extra element hidden by 'hide-in-detail' attribute will be shown again.
     */
    unlockInterface() {
        reactivateControls();
    }

    /**
     * IMPORTANT: To be used after the data is has been recovered in a Vue component or via Javascript AJAX call.
     * Show the elements with the 'show-in-detail' attribute.
     */
    showActionButtons() {
        showAfterLoading();
    }

    // #endregion

    // #region Use of Controls

    avoidUpAndDownArrowKeys(event) {
        return preventUpAndDownArrowKeys(event);
    }

    /**
     * Prevent that a negative symbol can be inserted in an input.
     * @param { Event } event Javascript Event.
     */
    preventNoIntegerNumbers(event) {
        preventNegativeNumbers(event);
    }

    /**
     * Only accepts numbers from 0 to 9. If its permitted the funtions returns true. If not preventDefault.
     * @param { Event } event Javascript Event.
     */
    admitOnyIntegersAndPositiveNumbers(event) {
        admitOnyIntegersAndPositiveNumbers(event);
    }

    /**
     * Find the input in the row and checked as true. Works with radioButtons, not with checkbox.
     * @param { Event } event Javascript Event.
     */
    assignRowAsSelectableRadioButton(event) {
        makeRowSelectableRadioButtons(event);
    }

    /**
     * Find the input in the row and checked as true. Works with radioButtons, not with checkbox.
     * @param { Event } event Javascript Event.
     * @param { bool } checkValue Indicates if its checked or not.
     */
    assingRowAsSelectableCheckBox(event, checkValue) {
        makeRowSelectableCheckBox(event, checkValue);
    }

    // #endregion

    // #region UI - various

    /**
     * Click the button of the Main menu to collapse or uncollapse it.
     */
    clickHamburguerButton() {
        clickMainMenu();
    }

    /**
     * Force the closure of the OffCanvas open if there is any.
     */
    closeAllOffCanvas() {
        closeOffCanvasWithCloseButton();
    }

    clickElement(id) {
        $("#" + id).click()
    }

    //Show spinner and block background
    showSpinner(loading) {
        showSpinner(loading);
    }

    // #endregion

}