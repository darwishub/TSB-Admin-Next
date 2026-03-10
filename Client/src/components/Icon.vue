<template>
    <div class="icon-tsb">
        <Ionicon :icon_name="icon_name" :class="$attrs.class" :font_size="font_size"/>
    </div>
</template>

<script setup>
import Ionicon from "@/components/Ionicon.vue"
defineProps({
    icon_name: {
        type: String
    },
    font_size: {
        type: String
    }
});
</script>

<style scoped>
.icon-tsb {
    line-height: 0;
}
.icon-tsb ion-icon {
    padding: 0.75rem;
    border-radius: 100%;
}
.icon-tsb ion-icon.disabled {
    background: #F5F5F5;
}
.icon-tsb ion-icon.success {
    background: #ACE7A0;
} 
.icon-tsb ion-icon.danger {
    background: #FAADB4;
} 
.icon-tsb ion-icon.warning {
    background: #FFE3B0;
}
.icon-tsb ion-icon.green-2 {
    background: rgba(56, 171, 109, 0.5);
} 
.icon-tsb.white-bg ion-icon {
    background: #fff;
}
</style>