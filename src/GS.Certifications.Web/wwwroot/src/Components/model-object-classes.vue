<script>


    export class Transport {
        constructor(id, name, transportTypeName, transportModeId, allowsCargos, allowsPassengers, allowsStaterooms, allowsVehicles, singleUnit) {
            this.id = id;
            this.name = name; 
            this.transportTypeName = transportTypeName;
            this.transportModeId = transportModeId;
            this.allowsCargos = allowsCargos;
            this.allowsPassengers = allowsPassengers;
            this.allowsStaterooms = allowsStaterooms;
            this.allowsVehicles = allowsVehicles;
            this.singleUnit = singleUnit;
        }
    };

   
     
</script>