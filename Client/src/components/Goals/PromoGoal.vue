<template>
    
</template>

<style>


.step-number {
    border-radius: 50%;
    width: 48px;
    height: 48px;
    padding: 12px;
    text-align: center;
}

.step-number.complete {
    background: #FFE3B0;
    border: 1px solid #C69030;
}

.step-number.incomplete {
    background: #c2c2c2;
    border: 1px solid #c2c2c2;
}
</style>

<script setup>
import { ref, onMounted } from "vue"
import Button from "@/components/Button.vue";
import Ionicon from "@/components/Ionicon.vue";
import Badge from "@/components/Badge.vue"
import Testimony from "@/components/Testimony.vue"
import YTVideo from "@/components/Goals/YTVideo.vue"



onMounted(() => {
    
});
</script>