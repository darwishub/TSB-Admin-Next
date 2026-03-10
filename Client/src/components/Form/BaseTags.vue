<template>
    <div class="base-textarea">
        <label :for="idField" class="form-label f-game fs-20 base-grey">{{ label }} (max 3)<span class="text-danger"
                v-if="props.isRequired">*</span>
        </label>
        <tags 
            :placeholder="props.placeholder" 
            :duplicate-select-item ="false" 
            :limit="3" 
            @on-tags-changed="onChange"
            :add-tag-on-keys="[13, 188]"
            :tags="value"
        />
        <p>{{ errorMessage }}</p>
    </div>
</template>

<script setup>
import { ref, computed, onMounted, toRaw } from "vue"
import { useField } from 'vee-validate';
import { string, array } from "yup"
import tags from 'vue3-tags-input';

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
        type: Boolean
    },
    isDisabled: {
        type: Boolean,
        default: false
    }
});



const name = computed(() => props.name);
const fieldRules = computed(() => {
    return props.isRequired ? array().of(string().required().nullable()).min(1).required() : undefined;
});

const idField = computed(() => {
    return String(name.value).replace(/[^0-9a-z]/gi, '');
});

const {
    value,
    errorMessage,
    handleBlur,
    handleChange,
    meta,
} = useField(name.value, fieldRules.value, {
    initialValue: [],
});


const onChange = (result) => {
    handleChange(toRaw(result))
};

</script>
