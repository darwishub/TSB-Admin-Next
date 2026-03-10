<template>
    <div class="base-input-phone">
        <label :for="name" class="form-label f-game fs-20 base-grey">{{ label }}<span class="text-danger" v-if="props.isRequired">*</span></label>
        
    <VuePhoneNumberInput
        :name="name"
        v-model.number="inputValue"
        show-code-on-list
        color="info"
        @update="onUpdate($event)"
        :class="{ 'is-invalid': !!errorMessage, 'is-valid': meta.valid }"
    />
        <pre>{{  }}</pre>
        <div v-show="errorMessage || meta.valid"
            :class="{ 'invalid-feedback': !!errorMessage, 'valid-feedback': meta.valid }">
            {{ errorMessage || successMessage }}
        </div>
    </div>
</template>

<script>
export default {
  inheritAttrs: false
}
</script>

<script setup>
import { ref, computed } from 'vue';
import VuePhoneNumberInput from 'maz-ui/components/MazPhoneNumberInput'
import { useField } from 'vee-validate';
import { string } from "yup"

const props = defineProps({
    value: {
        type: String,
        default: '',
    },
    name: {
        type: String,
        required: true,
    },
    label: {
        type: String,
        required: true,
    },
    successMessage: {
        type: String,
        default: '',
    },
    placeholder: {
        type: String,
        default: '',
    },
    isRequired: {
        type: Boolean,
        default: false
    },
    isDisabled: {
        type: Boolean,
        default: false
    }
});

const name = computed(() => props.name);

const isValid = ref(false);

const onUpdate = (event) => {
    isValid.value = event.isValid;
}

const {
    value: inputValue,
    errorMessage,
    handleBlur,
    handleChange,
    meta,
} = useField(name.value, inputValue => {

    if (!inputValue && props.isRequired) {
        return 'this is a required field';
    }

    if(!isValid.value && props.isRequired){
        return "please input a valid phone number"
    }
    return true;
}, {
    initialValue: props.value,
});


</script>

<style scoped>
</style>
