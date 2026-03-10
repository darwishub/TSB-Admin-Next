<template>
  <div class="main-wrapper">
    <Navbar>
      <div class="container d-flex justify-content-center">
        <a class="navbar-brand" href="#">
          <img src="/main-logo.png" alt="" />
        </a>
      </div>
    </Navbar>
    <router-view v-slot="{ Component }">
    <transition name="fade" mode="out-in">
      <component :is="Component" />
    </transition>
  </router-view>
  </div>
</template>

<script setup>
import Navbar from "@/components/Navbar.vue";

</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.6s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>