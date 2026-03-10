<template>
    <span class="badge badge-tsb-custom">
        <slot></slot>
    </span>
</template>

<style scoped>
.badge.badge-tsb-custom.green {
    background: #ACE7A0;
    color: #3EA528;
}
.badge.badge-tsb-custom.blue {
    background: #95AACF;
    color: #294A83;
}
.badge.badge-tsb-custom.grey {
    background: #F5F5F5;
    color: #696969;
}
</style>