<template>
  <div class="container">
      <div class="row">
        <div class="col-12 mt-6 col-md-8 offset-md-2 col-lg-6 offset-lg-3 mx-auto mt-6">
          <div>
            <div class="verify-info text-center">
              <div class="verify-info__img mb-c-2">
                <img src="/undraw_letter.svg" class="w-100" />
              </div>
              <div class="verify-info__title">Verify your email</div>
              <div class="verify-info__body-text grey mb-c-2">
                We've sent a verification email to
                <span class="text-sm-bold text-success">{{ currentEmail }}</span
                >, you need to verify your email address and activate your account. The
                link in the email will expire in 24 hours.
              </div>
              <Button class="grey outline form-control" @click="submitResend"
              :loading="isLoading" :disabled="isDisabled"
              :is_success="isSuccess"
              :is_fail="isFail"
                >Resend email <Ionicon
                    icon_name="send"
                    class="ms-3"
                    font_size="24"
                /></Button
              >
              <span class="text-danger">{{ errorMessage }}</span>
              <div class="d-md-flex justify-content-md-center mt-c-2">Need help? ask at <span>&nbsp;info@thestartupbuddy.co</span></div>
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
import { onMounted, ref, inject } from "vue";
import queryString from "query-string";

const router = useRouter();
const axios = inject("axios");
const cookies = inject('$cookies');

let currentEmail = ref("");
let errorMessage = ref("");

onMounted(() => {
  currentEmail.value = cookies.get("tsb-email");
});

let isDisabled = ref(false);
let isLoading = ref(false);
let isSuccess = ref(false);
let isFail = ref(false);

const submitResend = async () => {
  isDisabled.value = true;
  isLoading.value = true;
  errorMessage.value = "";
  const params = queryString.stringify({
    email: cookies.get("tsb-email"),
    address: location.origin,
  });
  
  await axios.post("api/Auth/ResendConfirmEmail?" + params, {}, { validateStatus: false }).then((res) => {

    if(res.status === 200) {
      errorMessage.value = "";
      isSuccess.value = true;
    }else {
      errorMessage.value = res.data
      isFail.value = true
    }
  }).then(() => {
      isLoading.value = false;
    })
  .finally(() => {
    setTimeout(() => {
        isDisabled.value = false;
        isFail.value = false;
        isSuccess.value = false;
      }, 1000)
  }); 

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