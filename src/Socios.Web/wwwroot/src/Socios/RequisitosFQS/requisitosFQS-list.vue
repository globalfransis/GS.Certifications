<!-- <template>
    <div>
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <div class="row justify-content-center text-center">
                        <h5>Fonasba Quality Standard</h5>
                    </div>
                    <div class="row mt-4">
                        <div class="col-12">
                            <h6 class="mb-3">Requisitos mínimos para obtener el Fonasba Quality Standard:</h6>
                            <ul class="list-group">
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Ser un miembro plenario del Centro de Navegación con una antigüedad no menor a tres (3) años.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar una nota compromiso firmada por el representante legal o apoderado de la compañía, expresando su compromiso para cumplir con el Código de Conducta de FONASBA y con los estatutos y Código de Ética del Centro de Navegación.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar estatutos de la compañía, con la correspondiente inscripción en la Inspección General de Justicia o Registro Público de Comercio pertinente.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar copia del acta de asamblea y directorio con la designación de los cargos del órgano de administración, debidamente inscripta en la Inspección General de Justicia o Registro Público de Comercio pertinente.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Demostrar evidencia de la inscripción de la compañía ante la Aduana, Prefectura, Migraciones, Sanidad de Fronteras, Administraciones Portuarias y cualquier otra autoridad relevante.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar constancia de C.U.I.T. y una prueba de la registración de la compañía en las distintas categorías de tributación de la Administración Federal de Ingresos Públicos y órganos de recaudación tributaria provinciales correspondientes.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar una certificación contable emitida por el auditor externo de la compañía, y debidamente legalizado por el Consejo Profesional pertinente, estableciendo que la compañía:
                                    <ul class="mt-2">
                                        <li>Cumple con los procedimientos y regulaciones contables de la Argentina.</li>
                                        <li>Mantiene cuentas separadas para cada uno de sus principales.</li>
                                        <li>Cumple con todas las obligaciones impositivas y previsionales.</li>
                                        <li>Cuenta con un estado financiero solvente.</li>
                                    </ul>
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Demostrar evidencia de compromiso a procesos de calidad, a través de Certificación ISO 9001 o superior, con mención de las áreas de certificación.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar una copia de la Póliza de Seguros emitida por el ITIC o cualquier otro Club Mutual o Compañía de Seguros internacionalmente reconocida.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar un organigrama, indicando los miembros del staff que son profesionales (capitanes, contadores públicos, abogados, etc.) y aquellos que están autorizados para representar a la compañía ante la Aduana, Prefectura, Sanidad de Fronteras, Migraciones, autoridades portuarias y otras autoridades.
                                </li>
                                <li class="list-group-item">
                                    <i class="bi bi-check-circle-fill text-success me-2"></i>
                                    Presentar una declaración demostrando la política de capacitación del personal de la compañía, más una copia de los programas anuales de entrenamiento.
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template> -->

<template>
    <div>
        <div class="col-12">
            <div class="card">
                <div class="card-body">
                    <div class="row justify-content-center text-center">
                        <h5> {{ loc["Requisitos"] }} </h5>
                        <h5> {{ loc["Prueba"] }} </h5>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>

import loc from "@/common/commonLoc.js"

export default {
    name: "requisitosFQS-list",
    components: {
    },
    data: function() {
        return {
            loc
        }
    },
}

</script>