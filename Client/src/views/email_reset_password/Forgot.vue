<template>
  <div class="container">
      <div class="row">
        <div
          class="
            col-12 mt-6 col-md-8 offset-md-2 col-lg-6 offset-lg-3 mx-auto
          "
        >
          <div class="forgot-info text-center">
            <div class="forgot-info__img mb-c-2">
                <img src="/undraw_forgot.svg" class="w-100" />
              </div>
            <div class="forgot-info__title text-sm-bold">
              Forgot your password?
            </div>
            <div class="forgot-body grey mb-c-2">
              Don't worry! Resetting your password is easy. Just type in the email you used to register to The Startup Buddy.
            </div>
            <Form :action="submitForgot">
              <div class="mb-c-2">
                <div class="form-floating">
                  <Input
                    placeholder="Email"
                    type="email"
                    name="email"
                    v-on="{ focus: resetErrorMessage, keypress: resetErrorMessage, keydown: resetErrorMessage }"
                    v-model="forgotModel.email"
                    :class="[
                      forgotModel.email != '' && forgotModel.email != null
                        ? isEmailValid
                          ? ''
                          : 'is-invalid'
                        : '',
                    ]"
                    required
                  >
                    <label>Email</label>
                    <div class="invalid-feedback">
                      {{ ErrorMessages }}
                    </div>
                  </Input>
                </div>
              </div>
              <Button class="green mb-c-2 form-control" type="submit"
              :loading="isLoading" :disabled="isDisabled" :is_success="isSuccess" :is_fail="isFail">Send reset link<Ionicon
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
import { reactive, ref, inject } from "vue";
import queryString from "query-string";

const axios = inject("axios");
const forgotModel = reactive({
  email: "",
});

const isEmailValid = ref(true);
const ErrorMessages = ref("");
let isDisabled = ref(false);
let isLoading = ref(false);
let isSuccess = ref(false);
let isFail = ref(false);

const resetErrorMessage = () => {
    isEmailValid.value = true;
    ErrorMessages.value = "";
}
const submitForgot = async () => {
  isDisabled.value = true;
  isLoading.value = true;

  const params = queryString.stringify({
    email: forgotModel.email,
    address: location.origin,
  });

  await axios.post("api/Auth/ForgotPassword?" + params, {}, { validateStatus: false }).then((res) => {
    if (res.status === 200 && forgotModel.email) {
      isSuccess.value = true;
      isEmailValid.value = true;
      ErrorMessages.value = "";
    } else {
      isFail.value = true;
      isEmailValid.value = false;
      ErrorMessages.value = res.data;
    }
  }).then(() => {
      isLoading.value = false;
  }).finally(() => {
    setTimeout(() => {
        isDisabled.value = false;
        isFail.value = false;
        isSuccess.value = false;
      }, 1000)
  });

};
</script>

<style scoped>
.forgot-info__title {
  font-size: 24px;
}
.forgot-info .forgot-info__img img {
  max-width: 200px;
  max-height: 200px;
}
</style>