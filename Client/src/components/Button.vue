<template>
  <button
    class="btn btn-default"
    :class="{
      'button--loading': loading,
      'is-success': is_success,
      'is-fail': is_fail,
    }"
    :type="type"
    ref="btnRef"
  >
    <div class="button__text">
      <slot />
    </div>
  </button>
</template>

<script setup>
import { ref } from "vue"

defineProps({
  type: {
    type: String,
  },
  loading: {
    type: Boolean,
    default: false,
  },
  is_success: {
    type: Boolean,
    default: false,
  },
  is_fail: {
    type: Boolean,
    default: false,
  },
});

const btnRef = ref();

defineExpose({
  btnRef
});

</script>

<style scoped>
.btn-game {
    border: 3px solid #FFE3B0 !important;
}
.btn {
  position: relative;
}
.button--loading .button__text,
.is-success .button__text,
.is-fail .button__text {
  visibility: hidden;
  opacity: 0;
}

.button--loading::after {
  content: "";
  position: absolute;
  width: 24px;
  height: 24px;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  margin: auto;
  border: 3px solid #c2c2c2;
  border-top-color: #fafafe;
  border-radius: 50%;
  animation: button-loading-spinner 0.6s linear infinite;
}

button.btn.is-success {
  pointer-events: none;
  background-color: #5bba47 !important;
  box-shadow: 0px 4px 8px rgba(91, 186, 71, 0.35) !important;
  border: none !important;
  color: #fff !important;
}
button.btn.is-fail {
  pointer-events: none;
  background-color: #d7525f !important;
  box-shadow: 0px 4px 8px rgba(215, 82, 95, 0.35) !important;
  border: none !important;
  color: #fff !important;
}
button.btn.is-success:after {
  content: "\F633";
  font-family: "bootstrap-icons";
  position: absolute;
  left: 0;
  right: 0;
  font-size: 24px;
  margin-left: auto;
  margin-right: auto;
  transform: translate(0, -84%);
  animation: 1s loading-btn--fade-in;
  animation-fill-mode: forwards;
}

button.btn.is-fail:after {
  content: "\F659";
  font-family: "bootstrap-icons";
  position: absolute;
  left: 0;
  right: 0;
  font-size: 24px;
  margin-left: auto;
  margin-right: auto;
  transform: translate(0, -84%);
  animation: 1s loading-btn--fade-in;
  animation-fill-mode: forwards;
}

@keyframes fadeIn {
  0% {
    opacity: 0;
  }
  100% {
    visibility: visible;
    opacity: 1;
  }
}

@-webkit-keyframes loading-btn--fade-in {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
@keyframes loading-btn--fade-in {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}

@keyframes button-loading-spinner {
  to {
    transform: rotate(360deg);
  }
}
/*== Gold ==*/
.btn.gold, .btn.gold:hover, .btn.gold:focus {
  background: linear-gradient(267.87deg, #FFB543 39.64%, #C69030 97.32%);
  box-shadow: 0px 4px 8px rgba(255 181 67 / 50%);
  color: #fff;
  border: none;
}
/*========================= green ==*/
.btn.green {
  background-color: #0f934c;
  box-shadow: 0px 4px 8px rgb(15 147 76 / 35%);
  color: #fff;
  border: none;
}
.btn.green:hover {
  background-color: #077e3e;
  color: #fff;
  box-shadow: 0px 4px 8px rgb(0 108 50 / 35%);
}
.btn.green:focus {
  background-color: #0f934c;
  outline: none;
  box-shadow: 0px 0px 0px 3px rgba(15, 147, 76, 0.25);
}

/*== green outline ==*/
.btn.green.outline {
  background-color: #fff;
  box-shadow: 0px 4px 8px rgb(15 147 76 / 35%);
  color: #0f934c;
  border: 1px solid #0f934c;
}
.btn.green.outline:hover {
  background-color: #077e3e;
  color: #fff;
  box-shadow: 0px 4px 8px rgb(0 108 50 / 35%);
}
.btn.green.outline:focus {
  background-color: #0f934c;
  outline: none;
  color: #fff;
  box-shadow: 0px 0px 0px 3px rgba(15, 147, 76, 0.25);
}

/*========================== grey ==*/
.btn.grey {
  background-color: #969696;
  box-shadow: 0px 4px 8px rgba(150, 150, 150, 0.35);
  color: #fff;
  border: none;
}
.btn.grey:hover {
  background-color: #696969;
  color: #fff;
  box-shadow: 0px 4px 8px rgba(105, 105, 105, 0.35);
}
.btn.grey:focus {
  background-color: #969696;
  outline: none;
  box-shadow: 0px 0px 0px 3px rgb(105 105 105 / 25%);
}
/*outline*/
.btn.grey.outline {
  background-color: #fff;
  box-shadow: 0px 4px 8px rgba(150, 150, 150, 0.35);
  color: #969696;
  border: solid 1px #969696;
}
.btn.grey.outline:hover {
  background-color: #696969;
  color: #fff;
  box-shadow: 0px 4px 8px rgba(105, 105, 105, 0.35);
}
.btn.grey.outline:focus {
  background-color: #969696;
  outline: none;
  color: #fff;
  box-shadow: 0px 0px 0px 3px rgb(105 105 105 / 25%);
}
/*========================= red ==*/
.btn.red {
  background-color: #d7525f;
  box-shadow: 0px 4px 8px rgba(215, 82, 95, 0.35);
  color: #fff;
  border: none;
}
.btn.red:hover {
  background-color: #bf2e3c;
  color: #fff;
  box-shadow: 0px 4px 8px rgba(191, 46, 60, 0.35);
}
.btn.red:focus {
  background-color: #d7525f;
  outline: none;
  box-shadow: 0px 0px 0px 3px rgba(191, 46, 60, 0.25);
}

.btn.red.outline {
  box-shadow: 0px 4px 8px rgba(215, 82, 95, 0.35);
  color: #d7525f;
  border: solid 1px #d7525f;
  background-color: #fff;
}
.btn.red.outline:hover {
  background-color: #bf2e3c;
  color: #fff;
  box-shadow: 0px 4px 8px rgba(191, 46, 60, 0.35);
}
.btn.red.outline:focus {
  background-color: #d7525f;
  outline: none;
  color: #fff;
}

/*========================= orange ==*/
.btn.orange {
    background-color: #DFAE55;
    box-shadow: 0px 4px 8px rgba(223, 174, 85, 0.35);
    color: #fff;
    border: none;
}

.btn.orange:hover {
    background: #C69030;
    color: #fff;
    box-shadow: 0px 4px 8px rgba(198, 144, 48, 0.35);
}

.btn.orange:focus {
    background-color: #DFAE55;
    outline: none;
    box-shadow: 0px 4px 8px rgba(223, 174, 85, 0.25);
}
/*=========================== Blue ==*/
/*outline*/
.btn.blue.outline {
  background-color: #fff;
  box-shadow: 0px 4px 8px rgba(65, 96, 148, 0.35);
  color: #416094;
  border: solid 1px #416094;
}
.btn.blue.outline:hover {
  background-color: #416094;
  color: #fff;
  box-shadow: 0px 4px 8px rgba(65, 96, 148, 0.35);
}
.btn.blue.outline:focus {
  background-color: #294A83;
  outline: none;
  color: #fff;
  box-shadow: 0px 4px 8px rgba(41, 74, 131, 0.35);
}
.btn.blue {
    background-color: #416094;
    box-shadow: 0px 4px 8px rgba(65, 96, 148, 0.35);
    color: #fff;
    border: none;
}
.btn.blue:hover {
    background: #294A83;
    color: #fff;
    box-shadow: 0px 4px 8px rgba(65, 96, 148, 0.35);
}
.btn.blue:focus {
    background-color: #416094;
    outline: none;
    box-shadow: 0px 4px 8px rgba(41, 74, 131, 0.35);
}

/*========================= white ==*/
.btn.white {
    background-color: #fff;
    box-shadow: 0px 4px 12px rgba(22, 34, 51, 0.12), 0px 16px 32px rgba(65, 96, 148, 0.16) !important;
    color: #8B9EB0;
    border: none;
}

.btn.white:hover {
    background: #fff;
    color: #8B9EB0;
    box-shadow: 0px 4px 12px rgba(22, 34, 51, 0.12), 0px 16px 32px rgba(65, 96, 148, 0.16) !important;
}

.btn.white:focus {
    background-color: #fff;
    outline: none;
    box-shadow: 0px 4px 12px rgba(22, 34, 51, 0.12), 0px 16px 32px rgba(65, 96, 148, 0.16) !important;
}
</style>