const LayoutMode = {
    Split: {
        apply: function (fileElemId, formElemId) {
            document.getElementById(fileElemId).classList.add("col-6");
            document.getElementById(fileElemId).classList.add("col-md-6");
            document.getElementById(fileElemId).classList.remove("col-12");
            document.getElementById(fileElemId).style.height = ''
            document.getElementById(fileElemId).removeAttribute("hidden");
            document.getElementById(formElemId).classList.add("col-6");
            document.getElementById(formElemId).classList.add("col-md-6");
            document.getElementById(formElemId).classList.remove("col-12");
            document.getElementById(formElemId).removeAttribute("hidden");
        }
    },
    File: {
        apply: function (fileElemId, formElemId) {
            document.getElementById(fileElemId).classList.remove("col-6");
            document.getElementById(fileElemId).classList.remove("col-md-6");
            document.getElementById(fileElemId).style.height = '100vh'
            document.getElementById(fileElemId).classList.add("col-12");
            document.getElementById(fileElemId).removeAttribute("hidden");
            document.getElementById(formElemId).setAttribute("hidden", "true");
        }
    },
    Form: {
        apply: function (fileElemId, formElemId) {
            document.getElementById(formElemId).classList.remove("col-6");
            document.getElementById(formElemId).classList.add("col-12");
            document.getElementById(formElemId).removeAttribute("hidden");
            document.getElementById(fileElemId).setAttribute("hidden", "true");
        }
    }
}

export default LayoutMode;