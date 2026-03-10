<template>
  <div class="container">
      <div class="row">
        <div class="col-12 mt-6 col-md-8 offset-md-2 col-lg-6 offset-lg-3 mx-auto mt-6">
          <div>
            <div class="verify-info text-center">
              <div class="verify-info__img mb-c-2">
                <img src="/undraw_taken.svg" class="w-100" />
              </div>
              <div class="verify-info__title text-danger">
                Email verification link expired.
              </div>
              <div class="verify-info__body-text grey mb-c-2">
                Hi there! Your verification link has expired because you haven't used it in time. The verification link expires after 24 hours and can only be used once. You can recreate your link by clicking on the "Request Another link" button.
              </div>
              <Button class="red mb-c-2 form-control" @click="submitRequest"
                >Request Another link
                <Ionicon icon_name="repeat" class="ms-3" font_size="24"
              /></Button>
              <div class="d-md-flex grey justify-content-md-center">
                If this issue continues to persist, let us know at
                info@thestartupbuddy.co
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<script setup>
import Navbar from "@/components/Navbar.vue";
import Ionicon from "@/components/Ionicon.vue";
import Icon from "@/components/Icon.vue";
import Button from "@/components/Button.vue";
import { useRouter } from "vue-router";

const router = useRouter();

const submitRequest = () => {
  return router.push("/resend-verification");
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