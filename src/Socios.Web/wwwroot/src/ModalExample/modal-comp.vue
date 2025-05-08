<template>

    <div class="modal fade" modal-partial :id="idModal" data-backdrop="static" data-keyboard="false" tabindex="-1" :aria-labelledby="idModal + 'staticBackdropLabel'" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" :id="idModal + 'staticBackdropLabel'">
                        <slot name="header">
                            GSF * Reemplazar por nombre sistema
                        </slot>
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row justify-content-md-center">
                        <slot>****************</slot>
                    </div>
                </div>
                <div class="modal-footer">
                    <slot name="footer">
                        <h6>Footer slot</h6>
                    </slot>
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary">Ejecutar</button>
                </div>
            </div>
        </div>
    </div>

</template>

<script>

    export default {
        name: "modal-comp",
        props: {
            idModal: String,
        },
    };

</script>