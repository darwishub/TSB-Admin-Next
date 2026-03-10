<template>
  <div class="container">
      <div class="row">
        <div
          class="col-12 mt-6 col-md-8 offset-md-2 col-lg-6 offset-lg-3 mx-auto">
          <div class="resend-verification-info text-center">
            <div class="resend-verification-info__img mb-c-2">
                <img src="/undraw_notify.svg" class="w-100" />
              </div>
            <div class="resend-verification-info__title text-sm-bold">
              Your email has not been verified?
            </div>
            <div class="resend-verification-body grey mb-c-2">
              Don't worry, we will help you resend the verification link to your
              email.
            </div>
            <Form :action="submitResendVerification">
              <div class="mb-c-2">
                <div class="form-floating">
                  <Input placeholder="Email" 
                  type="email"
                  v-on="{ focus: resetErrorMessage, keypress: resetErrorMessage, keydown: resetErrorMessage }"
                  v-model="resendModel.email"
                  name="email"
                  :class="[
                      resendModel.email != '' && resendModel.email != null
                        ? isEmailValid
                          ? ''
                          : 'is-invalid'
                        : '',
                    ]"
                  required>
                    <label>Email</label>
                    <div class="invalid-feedback">
                      {{ errorMessage }}
                    </div>
                  </Input>
                </div>
              </div>
              <Button class="green mb-c-2 form-control" type="submit"
              :loading="isLoading" :disabled="isDisabled"
              :is_success="isSuccess"
              :is_fail="isFail"
                >Resend verification<Ionicon
                  icon_name="send"
                  class="ms-3"
                  font_size="24"
              /></Button>
            </Form>
          </div>
          <div class="d-md-flex justify-content-md-center">
            <div>
              Don't have an account?<router-link to="/signup">
                Sign Up!</router-link
              >
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
import Form from "@/components/Form/Form.vue";
import Input from "@/components/Form/Input/Input.vue";
import { onMounted, inject, ref, reactive } from "vue";
import queryString from "query-string";
import { useRouter } from "vue-router";

const axios = inject("axios");
const router = useRouter();
let errorMessage = ref("");
const isEmailValid = ref(true);
const resendModel = reactive({
  email: ""
});
let isDisabled = ref(false);
let isLoading = ref(false);
let isSuccess = ref(false);
let isFail = ref(false);

const resetErrorMessage = () => {
  errorMessage.value = "";
  isEmailValid.value = true;
}
const submitResendVerification = async () => {
   isDisabled.value = true;
  isLoading.value = true;

  const params = queryString.stringify({
    email: resendModel.email,
    address: location.origin,
  });
  await axios.post("api/Auth/ResendConfirmEmail?" + params, {}, { validateStatus: false }).then((res) => {
    if (res.status === 200 && resendModel.email) {
      isEmailValid.value = true;
      errorMessage.value = "";
      isSuccess.value = true;
  
    } else {
      isEmailValid.value = false;
      errorMessage.value = res.data;
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
}

</script>

<style scoped>
.resend-verification-info__title {
  font-size: 24px;
}
.resend-verification-info .resend-verification-info__img img {
  max-width: 200px;
  max-height: 200px;
}
</style>