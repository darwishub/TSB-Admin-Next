<template>
  <div class="container">
  </div>
</template>

<script setup>
import { onMounted, inject } from "vue";
import queryString from "query-string";
import { useRouter } from "vue-router";

const axios = inject("axios");
const router = useRouter();

onMounted(async () => {
  const params = queryString.stringify({
    userId: router.currentRoute.value.query.userId,
    code: router.currentRoute.value.query.code,
  });

  await axios
    .post("api/Auth/EmailVerification?" + params, {}, { withCredentials: true })
    .then((res) => {
      if (res.status === 200 && res.data == "success") {
        return router.push({ path: "/verification-successful" });
      } else {
        return router.push({ path: "/expired-confirm-email" });
      }
    });
});
</script>