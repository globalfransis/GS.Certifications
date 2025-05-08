

window.onbeforeunload = function () {
    //Just before leave the page, disable user interaction
    UserInteraction.lockSubmit();
    //Needed to not show browser default dialog
    return;
}

removeSpinners = function() {
    $("span[spinner]").remove();
}

$(function () {
    $("button[type='submit'], input[type='submit']").click((e) => {
        var $button = $(e.currentTarget);
        var $htmlButton = $button.html();
        $button.html('<span spinner class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> ' + $htmlButton);    
    })
    
    //The button is disabled but, just in case, we store de form token in sessionStorage and prevent the user from sending again.
    $('form').submit(function () {
        const _aftKey = 'aft';
        var $form = $(this);
        var aftFormValue = $form.find("input[name='__RequestVerificationToken']").val();
        var aftDbValue = sessionStorage.getItem(_aftKey);
        if (aftDbValue === null || aftDbValue !== aftFormValue) {
            sessionStorage.setItem(_aftKey, aftFormValue);
            //add spinner to submit button
            var $button = $("form").find("button[type='submit']");
            return true;
        }
        else {
            alert("Los datos ya están en proceso");
            return false;
        }
    });
});

class UserInteraction {
    static lockSubmit = function () {
        $("form button").prop("disabled", true);
    }
}
