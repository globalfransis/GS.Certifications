export default {
    data() {
        return {
        };
    },
    methods: {
    },
    async mounted() {
        // Se maneja el evento 'popstate' el cual es lanzado cuando navegamos hacia atrás
        // El manejo consiste en agregar el parametro fromDetail
        window.onpopstate = function (event) {
            let currentHash = window.location.hash;

            const param = '?fromDetail=true';
            if (!currentHash.endsWith(param)) {
                currentHash += param
            }

            window.location.hash = currentHash;
        };

    },
};