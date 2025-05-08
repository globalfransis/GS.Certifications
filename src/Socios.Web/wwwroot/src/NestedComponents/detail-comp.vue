<template>
    <div>
        <slot>

        </slot>
        <br />
        <div class="d-flex">

            <br />
            <div v-for="item in details">
                <div class="card" style="width: 18rem;">
                    <img v-bind:src="item.url" class="card-img-top" />
                    <div class="card-body">
                        <h5 class="card-title">{{item.name}}</h5>
                        <p class="card-text">{{item.description}}</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
    .card {
        margin: 5px;
    }
</style>

<script>

    export default {
        name: "detail-comp",
        data() {
            return {
                details: [
                    { url: "https://picsum.photos/300/180?ramdom=1", name: "Lore inpectur", description: "At varius vel pharetra vel turpis nunc eget lorem dolor sed viverra ipsum nunc aliquet" },
                    { url: "https://picsum.photos/300/180?ramdom=2", name: "Magnis dis parturient", description: "Magnis dis parturient montes nascetur ridiculus mus mauris vitae ultricies leo integer malen" },
                    //{ url: "https://picsum.photos/300/180?ramdom=3", name: "Velit ut tortor", description: "Mretium fusce id velit ut tortor pretium viverra suspendisse potenti nullam ac tortor vitae" },
                    { url: "https://picsum.photos/300/180?ramdom=4", name: "Magnis dis parturient", description: "Magnis dis parturient montes nascetur ridiculus mus mauris vitae ultricies leo integer malen" },
                ]
            }
        }
    };

</script>
