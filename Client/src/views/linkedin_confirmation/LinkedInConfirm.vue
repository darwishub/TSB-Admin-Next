<template>
  <div class="container">
    <div class="col-12 mt-6 col-md-8 offset-md-2 col-lg-6 offset-lg-3 col-xl-4 offset-xl-4 mx-auto mt-6">
      <div class="linkedin-confirm-info__title text-sm-bold">Checking authentication...</div>
    </div> 
  </div>
</template>

<script setup>
import { reactive, ref, inject, onMounted } from "vue";
import queryString from "query-string";
import { useRouter } from "vue-router";
import { supabase } from "@/supabase";

const axios = inject("axios");
const router = useRouter();
const cookies = inject("$cookies");

let errorMessage = ref("");

onMounted(async () => {
  if (router.currentRoute.value.query.error_description) {
    router.push("/signin");
  }

  if (router.currentRoute.value.query.refresh == "true") {
    if (router.currentRoute.value.query.purpose == "signup") {
      setTimeout(async () => {
        const getState = JSON.parse(router.currentRoute.value.query.state);
        const session = await supabase.auth.session();
        let convertDurationType = getState.membership_duration ? 1 : 0;

        const params = queryString.stringify({
          provider_id: session.user.user_metadata.provider_id,
          name_user: session.user.user_metadata.name,
          role: getState.role,
          token: getState.token,
          membership_type: parseInt(getState.membership_type),
          coupon: getState.coupon,
          membership_duration: convertDurationType,
          email: session.user.email,
          programcode: getState.programcode,
          skip_trial: getState.skip_trial,
          payment_model: getState.payment_model,
          card_valid: getState.card_valid,
          program_valid: getState.program_valid,
        });

        await axios
          .post(
            "api/LinkedInAuth/LinkedInSignUp?" + params,
            {},
            { validateStatus: false }
          )
          .then((res) => {
            if (res.status === 200) {
              cookies.set("tsb-auth", res.data);
              router.push("/home");
            } else {
              alert(res.data);
            }
          });

        router.push("/signin");
      }, 1000);
    } else if (router.currentRoute.value.query.purpose == "signin") {
      setTimeout(async () => {
        const session = await supabase.auth.session();
        await axios
          .post(
            "api/LinkedInAuth/LinkedInSignIn?email=" + session.user.email + "",
            {},
            { validateStatus: false }
          )
          .then((res) => {
            if (res.status === 200) {
              cookies.set("tsb-auth", res.data);
              router.push("/home");
            } else {
              alert(res.data);
            }
          });
        router.push("/signin");
      }, 1000);
    }
  } else {
    router.push("/signin");
  }
});
</script>

<style scoped>
.linkedin-confirm-info__title {
  font-size: 24px;
}
</style>