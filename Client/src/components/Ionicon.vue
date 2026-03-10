<template>
  <ion-icon
    :name="icon_name"
    :style="{ fontSize: font_size + 'px' }"
    class="md hydrated"
  ></ion-icon>
</template>

<script setup>
defineProps({
  icon_name: {
    type: String,
  },
  font_size: {
    type: String,
  },
});
</script>

<style scoped>
ion-icon {
  pointer-events: none;
  vertical-align: bottom;
}
ion-icon.disabled {
  color: #696969;
}
ion-icon.success {
  color: #3ea528;
}
ion-icon.danger {
  color: #BF2E3C;
}
ion-icon.green-2 {
  color: #077e3e;
}
ion-icon.info {
  color: #294A83;
}
ion-icon.grey {
  color: #6E8AA6;
}
ion-icon.grey-light-1 {
  color: #c2c2c2;
}
ion-icon.warning {
  color: #C69030;
}
ion-icon.white {
  color: #fff;
}
</style>