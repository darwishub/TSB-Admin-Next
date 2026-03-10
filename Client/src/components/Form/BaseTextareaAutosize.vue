<template>
    <div class="base-textarea-autosize">
        <label class="form-label f-game fs-20 base-grey">{{ label }}<span class="text-danger" v-if="props.isRequired">*</span></label>
        <button type="button" class="btn btn-default btn-add-text w-100 mb-3" @click="addText" :id="idField" v-if="!props.isDisabled">
            <div class="d-flex justify-content-between align-items-center">
                <div>{{ props.textButton }}</div>
                <img src="/add-icon.svg">
            </div>
        </button>
        <div class="mb-3" v-for="(field, idx) in fields" :key="field.key">
            <Field 
                :name="`${name}[${idx}]`" 
                v-slot="{
                field, 
                errorMessage, 
                meta
                }">
                <div class="textarea-autosize-wrapper position-relative">
                    <img src="/close-icon.svg" class="cursor-pointer position-absolute remove-btn" @click="remove(idx)" v-if="!props.isDisabled">
                    <resize-textarea 
                        v-bind="field" 
                        :id="idField" 
                        :rows="1"
                        :cols="0"
                        :placeholder="props.placeholder"
                        class="form-control"
                        :disabled="props.isDisabled"
                        >
                    </resize-textarea>
                </div>
            </Field>
        </div>
        <div 
                    v-show="errorMessage || meta.valid && meta.touched"
                    class="text-danger text-bold fs-14 mt-1">
                    {{ errorMessage || successMessage }}
                </div>
        <!-- <div v-show="!hasTextFields" class="fs-14 text-bold text-danger">this field is required, add at least one text field.</div> -->
    </div>
</template>

<script setup>
import { ref, computed, toRefs } from "vue"
import { Field, useFieldArray, useField } from 'vee-validate';
import { array, object, string } from "yup"

const props = defineProps({
    options: {
        type: Array
    },
    type: {
        type: String,
        default: 'text',
    },
    label: {
        type: String,
        required: true,
    },
    name: {
        type: String,
        required: true,
    },
    maxData: {
      type: Number
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
    },
    textButton : {
        type: String,
        default: 'Add a text field'
    },
    ruleValidation: {
        type: [Function, Object]
    }
});

const name = computed(() => props.name);
const idField = computed(() => {
  return String(name.value).replace(/[^0-9a-z]/gi, '');
});
const { remove, push, fields } = useFieldArray(name.value);

const fieldRules = computed(() => {
    return props.isRequired ? array().of(string().nullable().required("text field can't be empty.")).required("this is a required field").min(1).required()
   : undefined;
});

const hasCustomeRule = computed(() => {
  return props.ruleValidation !== undefined && props.isRequired ? props.ruleValidation : fieldRules.value;
});

const { errorMessage, meta } = useField(name.value, hasCustomeRule.value, {
    validateOnMount: false,
});

const addText = () => {
    if(fields.value.length > 0) {
        if(fields.value.find(i => i.isLast).value != ""){
            push("");
        }
    } else {
        push("");
    }
}

</script>

<style scoped>
.base-textarea-autosize .textarea-autosize-wrapper {
    -webkit-border-radius: 20px;
    -moz-border-radius: 20px;
    border-radius: 1rem;
    border: none;
    color: #113A62;
    font-weight: 500;
    font-size: 14px;
}

.base-textarea-autosize .textarea-autosize-wrapper textarea {
    -webkit-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    -moz-box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    box-shadow: inset 4px 4px 4px rgb(65 96 148 / 20%);
    border-radius: 1rem;
}

.base-textarea-autosize textarea {
    padding: 1.5rem !important;
    border: none;
    background: #fff;
    font-size: 14px;
}

/* .base-textarea-autosize textarea.form-control:focus {
    box-shadow: none;
} */
.btn.btn-add-text {
    background: #FFFFFF;
    box-shadow: 0px 4px 12px -4px rgb(22 34 51 / 12%), 0px 16px 32px rgb(65 96 148 / 16%);
    border-radius: 16px;
    color: #8B9EB0;
    font-weight: 500;
    font-size: 14px;
    height: 50px;
    padding: 0.5rem 1.25rem;
}

.base-textarea-autosize .textarea-autosize-wrapper .remove-btn {
    right: 8px;
    top: 8px;
}

.base-textarea-autosize.label-fixed .label-textarea {
    position: sticky;
    top: 0;
    z-index: 1;
    background-color: #f2f2f2;
}
</style>