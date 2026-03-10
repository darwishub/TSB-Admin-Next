<template>
  <div class="container">
      <div class="row">
        <div
          class="
            col-12
            mt-6
            col-md-8
            offset-md-2
            col-lg-6
            offset-lg-3
            col-xl-4
            offset-xl-4
            mx-auto
          "
        >
          <div class="reset-info">
            <div class="reset-info__title text-sm-bold">
              Reset new password,
            </div>
            <div class="reset-body grey mb-c-2">
              Your new password must be different to previously used passwords.
            </div>

            <Form
              @submit="submitResetPassword"
              :validation-schema="schema"
              v-slot="{ errors }"
            >
              <div class="mb-3">
                <div class="mb-3">
                  <div class="form-floating">
                    <Input
                      placeholder="Password"
                      :type="passwordInputType"
                      name="password"
                      :class="{ 'is-invalid': errors.password }"
                    >
                      <label>Password</label>
                      <div class="invalid-feedback">
                        {{ errors.password }}
                      </div>
                    </Input>
                  </div>
                </div>
                <div class="mb-3">
                  <div class="form-floating">
                    <Input
                      placeholder="Confirm password"
                      :type="passwordInputType"
                      name="confirm_password"
                      :class="{ 'is-invalid': errors.confirm_password }"
                    >
                      <label>Confirm Password</label>
                      <div class="invalid-feedback">
                        {{ errors.confirm_password }}
                      </div>
                    </Input>
                  </div>
                </div>
                <div class="reset-info__show-pass">
                  <FormCheckBox
                    id="show-password"
                    class="me-2"
                    @change="showPassword"
                  >
                    <label for="show-password" class="user-select-none"
                      >Show password</label
                    >
                  </FormCheckBox>
                </div>
              </div>
              <Button
                class="green mb-c-2 form-control"
                type="submit"
                :disabled="isDisabled"
                :loading="isLoading"
                >Reset password<Ionicon
                  icon_name="refresh-circle"
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
import Input from "@/components/Form/Input/Input.vue";
import { reactive, ref, inject, onMounted } from "vue";
import FormCheckBox from "@/components/Form/FormCheck.vue";
import queryString from "query-string";
import { Form, Field } from "vee-validate";
import { useRouter } from "vue-router";
import * as yup from "yup";

const axios = inject("axios");
const router = useRouter();

//== Reset Validation
const schema = yup.object({
  password: yup.string().min(8).required("Password is a required field").matches(/^(?=.*[a-z])/, "Passwords must have at least one lowercase ('a'-'z')."),
  confirm_password: yup.string().required("Confirm password is a required field").oneOf([yup.ref("password")], "Passwords do not match"),
});
//== End Validation

let passwordInputType = ref("password");
let isDisabled = ref(false);
let isLoading = ref(false);
let isSuccess = ref(false);
let isFail = ref(false);

let showPassword = ($event) => {
  if ($event.target.checked === true) {
    passwordInputType.value = "text";
  } else {
    passwordInputType.value = "password";
  }
};

let submitResetPassword = async (values) => {

  isDisabled.value = true;
  isLoading.value = true;

  const params = queryString.stringify({
    userId: router.currentRoute.value.query.userId,
    code: router.currentRoute.value.query.code,
    password: values.password
  });

  await axios
    .post("api/Auth/ResetPassword?" + params, {},
        {
          validateStatus: (status) => {
            return status < 500;
          },
        })
    .then((res) => {
      if (res.status !== 200 && res.data != "success") {
        isFail.value = true
        return router.push("/expired-reset-email");
      } else {
        isSuccess.value = true;
        return router.push("/reset-password-successful");
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

onMounted(async () => {
  const params = queryString.stringify({
    userId: router.currentRoute.value.query.userId,
    code: router.currentRoute.value.query.code,
  });

  await axios
    .get("api/Auth/TokenValidity?" + params, {}, { validateStatus: false })
    .then((res) => {
      if (res.status !== 200 && res.data != true) {
        return router.push("/expired-reset-email");
      } else {
        return;
      }
    });
});
</script>

<style scoped>
.reset-info__title {
  font-size: 24px;
}
.reset-info .reset-info__show-pass {
  font-size: 12px;
}
</style>