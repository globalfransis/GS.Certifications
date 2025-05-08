const hamButton = document.getElementById('ham-button')
const hamButtonMobile = document.getElementById('ham-button-mobile')
const drawer = document.getElementById('drawer')
const drawerMobile = document.getElementById('drawer-mobile')
const slimDrawer = document.getElementById('slim-drawer')
const bigDrawer = document.getElementById('big-drawer')
const pagecontent = document.getElementById('pagecontent')
const navbar = document.getElementById('navbar')

var isOpen = localStorage.getItem("pinned") === 'true';
var isPinned = localStorage.getItem("pinned") === 'true';
//ExpandMenu(isOpen);
//PinMenu(isPinned);

//function ExpandMenu(expanded) {
//    if (expanded) {
//        drawer.classList.remove('is-closed')
//        slimDrawer.style.opacity = '0'
//        bigDrawer.style.opacity = '1'
//        bigDrawer.style.display = 'block';
//        isOpen = true
//    }
//    else { 
//        if (drawer) {
//            drawer.classList.add('is-closed');
//        }
//        if (slimDrawer) {
//            slimDrawer.style.opacity = '1';
//        }
//        if (bigDrawer) {
//            bigDrawer.style.opacity = '0';
//            bigDrawer.style.display = 'none';
//        }
//        isOpen = false;
//    }
//}

//function PinMenu(pin) {
//    if (pin) {
//        drawer.classList.add('pinned');
//        drawer.style.marginTop = '0'
//        pagecontent.style.marginTop = '0'
//        pagecontent.classList.remove('ms-5');
//        navbar.style.position = 'relative'
//    }
//    else {
//        if (drawer) {
//            drawer.classList.remove('pinned');
//            drawer.style.marginTop = '64px';
//        }
//        if (pagecontent) {
//            pagecontent.style.marginTop = '64px';
//            pagecontent.classList.add('ms-5');
//        }
//        if (navbar) {
//            navbar.style.position = 'fixed';
//        }
//    }
//    localStorage.setItem("pinned", pin);
//}


function PressMenuOptionCollapsed(expanded, optionId) {
    ExpandMenu(expanded);

    var options = $('button[optionIdBig]');
    options.each(
        function () {
            $(this).addClass("collapsed");
            $(this).parents(".accordion-item").find(".accordion-collapse").removeClass("show");
        })

    var selectedOption = $('button[optionIdBig=' + optionId + ']');
    selectedOption.removeClass("collapsed");
    selectedOption.parents(".accordion-item").find(".accordion-collapse").addClass("show");
}

hamButton?.addEventListener('click', () => {
    //clearTimeout(sideMenuCooldown);

    if (drawer.classList.contains('pinned')) {
        PinMenu(false)
        ExpandMenu(false)
    } else {
        if (drawer.classList.contains('is-closed')) {
            ExpandMenu(true)
            PinMenu(false)
        } else {
            PinMenu(true)
        }
    }

})

hamButtonMobile?.addEventListener('click', () => {
    //clearTimeout(sideMenuCooldown);

    if (drawerMobile.classList.contains('is-closed')) {
        drawerMobile.classList.remove('is-closed')

    } else {
        drawerMobile.classList.add('is-closed')

    }


})


$(function () {
    $('[data-toggle="tooltip"]').tooltip({ customClass: 'gsf-tooltip' });
})

const table = document.getElementById('fixed-table')
const actions = document.getElementById('fixed-table-actions')
console.log(table?.offsetHeight)

if (table?.offsetHeight >= 100) {
   actions?.classList.add('is-sticky-button')
} else {
   actions?.classList.remove('is-sticky-button')
}

$(function () {
    navbar.style.borderBottom = 'solid 3px #00acac';
})





