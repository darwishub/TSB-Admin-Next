<template>
<div>
    <BaseInput :name="`${name}.schema.props.label`" label="Question" placeholder="Enter question title here" type="text"
        type-rule="text" class="mb-3" isRequired />
    <BaseInput :name="`${name}.schema.props.placeholder`" label="Question placeholder" placeholder="Enter placeholder here"
        type="text" type-rule="text" class="mb-3" isRequired />

    <BaseSelect :name="`${name}.schema.props.type_rule`" :options="options" label="Choose Dropdown Type" 
        placeholder="Select on these options"
        @onSelect="contentToggle" 
        value-by="value" label-by="label" 
        :is-required="true" ref="selectRef" />

    <template v-if="currentToggle == 'text'">
        <BaseTextareaAutosize :name="`${name}.schema.option`" label="Add More Dropdown Choices" :text-button="props.textButton"
            :is-required="true"
            :rule-validation="array().of(string().nullable().required('text field can\'t be empty.')).required('this is a required field').min(1).max(10).required()" />
    </template>

    <template v-if="currentToggle == 'number'">
        <div v-for="(field, idx) in fields" :key="field.key">
            <BaseInput :name="`${name}.schema.option[${idx}]`" :label="`${idx > 0 ? 'Maximum' : 'Minimum'} Number`"
                :placeholder="`Enter slider ${idx > 0 ? 'maximum' : 'minimum'} number`" type="text" type-rule="number"
                :is-required="true" @getInputValue="onInput" :ref="(el) => (inputValueRef[idx] = el)"
                :rule-validation="idx > 0 ? MaxRule : MinRule" />
        </div>
        <BaseInput label="Number helper" :name="`${name}.schema.option`" type="text" class="d-none" />
    </template>

    <BaseInputSwitch :name="`${name}.schema.props.required`" label="Required" />
</div>
</template>

<script setup>
import BaseInput from "@/components/Form/BaseInput.vue"
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseDatePicker from "@/components/Form/BaseDatePicker.vue"
import BaseTextarea from "@/components/Form/BaseTextarea.vue"
import BaseInputSwitch from "@/components/Form/BaseInputSwitch.vue"
import BaseInputCheckbox from "@/components/Form/BaseInputCheckbox.vue"
import BasePickSlider from "@/components/Form/BasePickSlider.vue"
import BaseFileUploader from "@/components/Form/BaseFileUploader.vue"
import BaseTextareaAutosize from "@/components/Form/BaseTextareaAutosize.vue"
import { computed, ref, watch, toRefs, onMounted } from "vue"
import { useFieldArray, useFieldValue } from 'vee-validate';
import { number, array, string } from "yup"

const props = defineProps({
    name: {
        type: String,
    },
    textButton : {
        type: String,
        default: 'Add a text field'
    },
    typeRule : {
        type: String
    }
});

const { name, textButton, typeRule } = toRefs(props);
const currentToggle = ref(null);
const selectRef = ref();

const MaxRule = number().integer().required("this is a required field").max(100).test('Max_Number', 'Max Number must greater than Min Number', (value) => {
                        return value > inputValueRef.value[0]?.inputValueRef.value ? true: false
                });

const MinRule = number().integer().required("this is a required field").max(100).test('Min_Number', 'Min Number must less than Max Number', (value) => {
                        return value < inputValueRef.value[1]?.inputValueRef.value ? true: false 
                });
                    

const { remove, push, replace, fields } = useFieldArray(`${name.value}.schema.option`);
const options = [{ label: "Numbers", value: 'number' }, { label: "Custom choices max 10", value: 'text' }];
const contentToggle = (value) => {

    if(value == 'number'){
        currentToggle.value = 'number'
        replace([null, null]);
    } else if(value == 'text') {
        currentToggle.value = 'text'
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
    currentToggle.value = typeRule.value;
    
    if(currentToggle.value == 'number' && fields.value.length == 0) {
        replace([null, null]);
    }

    if(currentToggle.value == 'text' && fields.value.length == 0) {
        replace([]);
    }
    

});

</script>

<style scoped></style>