<template>
    <Button class="btn-circle"
        :style="{ 
            height: props.height + 'px',
            width: props.width + 'px' 
        }"
    >
        <div class="button__text">
            <slot/>
        </div>
    </Button>
</template>

<script setup>
import Button from "@/components/Button.vue"
import Ionicon from "@/components/Ionicon.vue"

const props = defineProps({
    height: {
        type: String
    },
    width: {
        type: String
    }
});

</script>

<style scoped>
.btn-circle {
    padding: 6px 0px;
    border-radius: 50%;
    text-align: center;
    font-size: 10px;
}
.btn-circle-shadow {
    box-shadow: 2px 2px 4px rgb(114 142 171 / 10%), 6px 6px 50px #ffffff, 4px 4px 40px rgb(111 140 176 / 61%) !important;
}
.btn-circle-shadow:focus {
    outline: solid 11px #fff !important;
}
.button--loading .button__text {
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
</style>