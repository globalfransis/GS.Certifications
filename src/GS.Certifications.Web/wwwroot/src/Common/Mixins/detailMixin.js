export default {
    data() {
        return {
        };
    },
    methods: {
    },
    async mounted() {
        document.querySelectorAll('.form-container').forEach(container => {
            container.querySelectorAll('*').forEach(child => {
                child.setAttribute('disabled', true);
            });
        });
    },
};