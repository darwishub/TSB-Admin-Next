<template>
    <div>
        <BaseSelect 
        :name="props.name" 
        :options="options" 
        :label="props.label" 
        :placeholder="props.placeholder" 
        @onSelect="contentToggle" 
        v-bind="{
                'value-by': options !== undefined ? valueBy : null,
                'label-by': options !== undefined ? labelBy : null,
            }"
        :is-required="props.isRequired" 
        ref="selectRef"
        :value="fieldCodeType"
        v-if="options !== undefined"
    />

        <template v-if="currentToggle == 1">
            <BaseTextareaAutosize 
                :name="name_option" 
                label="Add More Dropdown Choices" 
                :text-button="props.textButton" 
                :is-required="true" 
                :rule-validation="array().of(string().nullable().required('text field can\'t be empty.')).required('this is a required field').min(1).max(10).required()"    
                />
        </template>

        <template v-if="currentToggle == 0">
            <div v-for="(field, idx) in fields" :key="field.key">
                <BaseInput 
                    :name="`${name_option}[${idx}]`" 
                    :label="`${idx > 0 ? 'Maximum' : 'Minimum' } Number`" 
                    :placeholder="`Enter slider ${idx > 0 ? 'maximum' : 'minimum' } number`" 
                    type="text" 
                    type-rule="number"
                    :is-required="true"
                    @getInputValue="onInput"
                    :ref="(el) => (inputValueRef[idx] = el)"
                    :rule-validation="idx > 0 ? MaxRule : MinRule"
                />
            </div>
                <BaseInput 
                    label="Number helper"
                    :name="name_option" 
                    type="text" 
                    class="d-none"
                />
        </template>
    </div>
</template>

<script setup>
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseTextareaAutosize from "@/components/Form/BaseTextareaAutosize.vue";
import { ref, onMounted, watch, toRefs } from "vue"
import { useFieldArray, useFieldValue } from 'vee-validate';
import { number, array, string } from "yup"

const props = defineProps({
    name: {
        type: String
    },
    options: {
        type: Array
    },
    label: {
        type: String
    },
    placeholder: {
        type: String
    },
    labelBy: {
        type: String
    },
    valueBy: {
        type: String
    },
    isRequired: {
        type: Boolean,
        default: false
    },
    textButton : {
        type: String,
        default: 'Add a text field'
    },
    name_option: {
        type: String
    },
    fieldCodeType: {
        type: [Number, String]
    }, 
    optionValue: {
        type: Array
    }
});

const currentToggle = ref(null);
const selectRef = ref();
const { options, valueBy, labelBy, fieldCodeType, name_option, optionValue } = toRefs(props);
const MaxRule = number().integer().required("this is a required field").max(100).test('Max_Number', 'Max Number must greater than Min Number', (value) => {
                        return value > inputValueRef.value[0]?.inputValueRef.value ? true: false
                });

const MinRule = number().integer().required("this is a required field").max(100).test('Min_Number', 'Min Number must less than Max Number', (value) => {
                        return value < inputValueRef.value[1]?.inputValueRef.value ? true: false 
                });
                    

const { remove, push, replace, fields } = useFieldArray(name_option.value);

const contentToggle = (value) => {

    if(value == 0){
        currentToggle.value = 0
        replace([null, null]);
    } else if(value == 1) {
        currentToggle.value = 1
        replace([]);
    } else {
        currentToggle.value = null;
        replace([]);
    }
};

const inputValueRef = ref([]);
const onInput = (e) => {

    for(let i = 0; i < inputValueRef.value.length; i++){
        inputValueRef.value[i].handleChange(inputValueRef.value[i].inputValueRef.value);
    }

}

onMounted(() => {
    currentToggle.value = fieldCodeType.value;
    
    if(currentToggle.value == 0 && fields.value.length == 0) {
        replace([null, null]);
    }

    if(currentToggle.value == 1 && fields.value.length == 0) {
        replace([]);
    }

});

// watch(currentToggle, (newValue) => {
  
//     if(newValue == "number"){
//         currentToggle.value = "number"
//     } else if(newValue == "text") {
//         currentToggle.value = "text"
//     } else {
//         currentToggle.value = null;
//     }
  
// });

</script>
