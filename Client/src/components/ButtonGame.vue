<template>
    <button class="btn btn-default btn-game blue">
        <div class="text-button text-border f-game fs-16 ls-1">
            <slot />
        </div>
    </button>
</template>

<script setup>

</script>

<style scoped>
.btn-game {
    border: 3px solid #FFE3B0;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    border-radius: 0.5rem;
    padding: 0.25rem 0.5rem;
}
/*== Blue ==*/
.btn-game.blue {
    background: #416094;
}
.btn-game .text-button {
    color: #FFE3B0;
}
</style>