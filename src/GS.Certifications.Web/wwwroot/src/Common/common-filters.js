import Vue from 'vue';
import dayjs from 'dayjs'

String.prototype.toUiDate = function () {
    return dayjs(this).format('DD/MM/YYYY')
}

String.prototype.toUiDateTime = function () {
    return dayjs(this).format('DD/MM/YYYY HH:mm:ss')
}

Vue.filter('uidatetime', function (value) {
    if (!value) return ''
    return value.toUiDateTime()
})

Vue.filter('uidate', function (value) {
    if (!value) return ''
    return value.toUiDate()
})

Vue.filter('currency', function (value) {
    if (value != 0 && !value) return ''

    const formatter = new Intl.NumberFormat('es-AR', { style: 'currency', currency: 'ARS' });

    const formattedValue = formatter.format(value);

    return formattedValue;
})

Vue.filter('dollar', function (value) {
    if (value != 0 && !value) return '';

    const formatter = new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' });

    const formattedValue = formatter.format(value);

    return formattedValue;
});


Vue.filter('puntoVenta', function (puntoVenta) {
    puntoVenta = parseInt(puntoVenta) || 0;

    const pvFormateado = String(puntoVenta).padStart(5, '0');

    return pvFormateado;
});

Vue.filter('nroComprobante', function (nroComprobante) {
    nroComprobante = parseInt(nroComprobante) || 0;
    const nroComprobanteFormateado = String(nroComprobante).padStart(8, '0');

    return nroComprobanteFormateado;
});

Vue.directive('uppercase', {
    update(el) {
        el.value = el.value.toUpperCase()
    },
})
