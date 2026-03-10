<template>
  <div class="container">
      <div class="row">
        <div
          class="col-12 mt-6 col-md-8 offset-md-2 col-lg-6 offset-lg-3 mx-auto"
        >
          <div>
            <div class="verify-info text-center">
              <div class="verify-info__img mb-c-2">
                <img src="/undraw_awesome.svg" class="w-100" />
              </div>
              <div class="verify-info__title text-success">
                Account verified.
              </div>
              <div class="verify-info__body-text grey mb-c-2">
                You have successfully verified your account. Welcome to The Startup Buddy!
              </div>
              <Button class="green mb-c-2 form-control" @click="submitRequest"
                >Sign in now
                <Ionicon icon_name="log-in" class="ms-3" font_size="24"
              /></Button>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<script setup>
import Navbar from "@/components/Navbar.vue";
import Ionicon from "@/components/Ionicon.vue";
import Button from "@/components/Button.vue";
import { useRouter } from "vue-router";

const router = useRouter();

const submitRequest = () => {
  return router.push("/signin");
};
</script>

<style scoped>
.verify-info .verify-info__title {
  font-weight: 600;
  font-size: 24px;
}
.verify-info .verify-info__img img {
  max-width: 200px;
  max-height: 200px;
}
</style>