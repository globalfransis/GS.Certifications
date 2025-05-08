export default {
    data() {
        return {
            isSorting: false,
        };
    },
    methods: {
        async handleColumnSorting(data) {
            throw new Error("handleColumnSorting debe ser implementado en el componente que usa el mixin");
        },
        fixColumnSortingHTML(data) {
            const elementoConSort = document.querySelectorAll('[default-sort-datatable]');

            if (elementoConSort.length > 0) {
                // const valorDefaultSort = elementoConSort.getAttribute('default-sort-datatable');
                
                elementoConSort.forEach(element => {
                    element.removeAttribute('default-sort-datatable');
                });
            }
            const nuevoElemento = document.querySelector(`[data-column="${data.orderPropertyName}"]`);
            if (nuevoElemento) {
                nuevoElemento.setAttribute('default-sort-datatable', data.orderDirection === '0' ? 'asc' : 'desc');
            }
        },
    },
    async mounted() {
        const columnSortedHandlerClosure = async (ctx) => {
            return async (data) => {
                if (ctx.isSorting) return;
                ctx.isSorting = true;

                await ctx.handleColumnSorting(data);

                ctx.isSorting = false;
            };
        };

        const columnSortedHandler = await columnSortedHandlerClosure(this);

        document.getElementById(this.idTable).addEventListener('columnSorted', async (e) => {
            e.detail.orderDirection = e.detail.orderDirection == 'asc' ? '0' : '1';
            const columnSortedData = {
                orderDirection: e.detail.orderDirection,
                orderPropertyName: e.detail.columnName
            };


            await columnSortedHandler(columnSortedData);
        });
    },
};