<template>
  <div class="container">
      <div class="row">
        <div
          class="col-12 mt-6 col-md-6 offset-md-6 col-lg-4 offset-lg-4 mx-auto"
        >
          <div class="signin-intro text-sm-bold">Hello! Welcome back,</div>
          <div class="mb-c-2 grey">Insert your email to continue</div>

          <form @submit="signIn">

            <BaseInput 
              name="email" 
              label="Email" 
              class="mb-3" 
              placeholder="Your email" 
              :is-required="true" 
            />

            <BaseInput 
              name="password" 
              label="Password" 
              class="mb-3" 
              :type="passwordInputType"
              placeholder="Your password" 
              :is-required="true" 
            />

            <div>
            <div class="d-flex justify-content-between mb-3 signin-control">
              <div class="d-inline-block signin-control__show-pass">
                <BaseInputSwitch
                  name="show_password"
                  label="Show Password"
                  class="mb-3"
                  @onSwitchChange="showPassword"
                />
              </div>
            </div>

            <Button
              class="green mb-c-2 form-control rounded-md"
              type="submit"
              :disabled="isDisabled"
              :loading="isLoading"
              :is_success="isSuccess"
              :is_fail="isFail"
              >Sign in<Ionicon
                icon_name="log-in"
                class="ms-3"
                font_size="24"
              />
            </Button>
            </div>

          </form>

        </div>
      </div>
    </div>
</template>

<script setup>
import Navbar from "@/components/Navbar.vue";
import Input from "@/components/Form/Input/Input.vue";
import Button from "@/components/Button.vue";
import Ionicon from "@/components/Ionicon.vue";
import FormCheckBox from "@/components/Form/FormCheck.vue";
import Form from "@/components/Form/Form.vue";

import { adminAuthStore } from "@/stores/adminAuthStore"
import { storeToRefs } from 'pinia';

import BaseInput from "@/components/Form/BaseInput.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"

import { useRouter } from "vue-router";
import { ref, onMounted, inject } from "vue";
import { useForm, useFieldValue } from "vee-validate";
import { string, object, boolean } from "yup";

const { adminAuth } = storeToRefs(adminAuthStore());
const axios = inject("axios");
const router = useRouter();
// const cookies = inject("$cookies");

let passwordInputType = ref("password");
let showRoute = ref(true);

//== Validation
const signinSchema = object({
  email: string().required().email(),
  password: string().required()
});

const {
  errors,
  handleSubmit,
  setFieldError,
  setFieldValue,
  resetForm,
} = useForm({
  validationSchema: signinSchema,
});

//== End Validation

let showPassword = (e) => {
  if (e) {
    passwordInputType.value = "text";
  } else {
    passwordInputType.value = "password";
  }
};

const emailValue = useFieldValue("email");
const passwordValue = useFieldValue("password");


let isDisabled = ref(false);
let isLoading = ref(false);
let isSuccess = ref(false);
let isFail = ref(false);

const signIn = handleSubmit(async (values) => {
  isDisabled.value = true;
  isLoading.value = true;
  await axios
    .post(
      "api/Auth/SignIn?email=" +
        values.email +
        "&password=" +
        values.password +
        "",
      {},
      {
        validateStatus: (status) => {
          return status < 500;
        },
      }
    )
    .then((res) => {
      if (res.status === 200) {
        isLoading.value = false;
        isSuccess.value = true;

        // cookies.set("tsb-auth-admin", res.data);

        adminAuth.value.email = res.data.email;
        adminAuth.value.onBoardingStatus = res.data.onBoardingStatus;
        adminAuth.value.photo = res.data.photo;
        adminAuth.value.coins = res.data.coins;
        adminAuth.value.programId = res.data.programId;
        adminAuth.value.role = res.data.role;
        adminAuth.value.name = res.data.name;

        if (res.data.userOnBoardingStatus) {
            return router.push("/home");
          } else {
            return router.push("/user-onboarding");
          }

        } if (res.status === 404) {
          isLoading.value = false;
            isFail.value = true;
            showRoute.value = false;
            setFieldError(
                "email",
                "Account doesn't exist."
            );
        } else {
        isLoading.value = false;
        isFail.value = true;
          showRoute.value = false;
          setFieldError(
            "password",
            "The password you entered is incorrect. Please try again."
          );
      }
    })
    .finally(() => {
      setTimeout(() => {
        isDisabled.value = false;
        isFail.value = false;
        isSuccess.value = false;
      }, 1000)
    });
});

</script>

<style scoped>
.v-enter-active,
.v-leave-active {
  transition: opacity 0.25s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
.signin-intro {
  font-size: 24px;
}
.signin-control .signin-control__show-pass,
.signin-control .signin-control__forget {
  font-size: 12px;
}
</style>