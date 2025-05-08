<template>
    <div class="d-flex">
        <div>
            <div class="card" style="width: 18rem;">
                <img src="https://picsum.photos/300/180?ramdom=1" class="card-img-top"/>
                <div class="card-body">
                    <h5 class="card-title">{{name}}</h5>
                    <p class="card-text">{{description}}</p>
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
        props: {
            name: String,
            description: String
        }
    };

</script>
