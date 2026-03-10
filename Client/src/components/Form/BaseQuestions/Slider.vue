<template>
<div>
    <BaseInput :name="`${name}.schema.props.label`" label="Question" placeholder="Enter question title here" type="text"
        type-rule="text" class="mb-3" isRequired />

    <BaseSelect :name="`${name}.schema.field_code_type`" :options="options" label="Choose Dropdown Type" placeholder="Select on these options"
        is-required @onSelect="onSelect" ref="selectRef" 
        value-by="value" label-by="label" />

    <template v-if="currentToggle == 0">

        <BaseInput :name="`${name}.schema.props.type_rule`" type="hidden" class="d-none" value="number" />

        <div v-for="(field, idx) in fields" :key="field.key">
            <BaseInput :name="`${name}.schema.option[${idx}]`" :label="`${idx > 0 ? 'Maximum' : 'Minimum'} Number`"
                :placeholder="`Enter slider ${idx > 0 ? 'maximum' : 'minimum'} number`" type="text" type-rule="number"
                :is-required="true" @getInputValue="onInput" :ref="(el) => (inputValueRef[idx] = el)"
                :rule-validation="idx > 0 ? MaxRule : MinRule" />
        </div>
    </template>

    <template v-if="currentToggle == 1">
        <BaseInput :name="`${name}.schema.props.type_rule`" type="hidden" class="d-none" value="text" />

        <div v-for="(field, idx) in fields" :key="field.key">
            <BaseInput :name="`${name}.schema.option[${idx}]`" :label="`Choice #${idx + 1}/${fields.length}`"
                placeholder="Enter slider choice" type="text" :is-required="true" />
        </div>
    </template>
    <template v-if="currentToggle == 2">
        <BaseInput :name="`${name}.schema.props.type_rule`" type="hidden" class="d-none" value="text" />
        <div v-for="(field, idx) in fields" :key="field.key">
            <BaseInput :name="`${name}.schema.option[${idx}]`" :label="`Choice #${idx + 1}/${fields.length}`"
                placeholder="Enter slider choice" type="text" :is-required="true" />
        </div>
    </template>
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
import { useFieldArray } from 'vee-validate';
import { number } from "yup"

const props = defineProps({
    name: {
        type: String,
    },
    fieldCodeType: {
        type: Number
    }
});

const { name, fieldCodeType } = toRefs(props);
const currentToggle = ref(null);
const selectRef = ref();
const inputValueRef = ref([]);
const options = [{ label: "Custom 3 Choices", value: 1 }, { label: "Custom 5 Choices", value: 2 }, { label: "Numbers", value: 0 }];
const { remove, push, replace, fields } = useFieldArray(`${name.value}.schema.option`);

const onSelect = (value) => {
    currentToggle.value = value;
    if(value == 0) {
        replace([null, null]);
    }else if(value == 1) {
        replace(["", "", ""]);
    } else if(value == 2){
        replace(["", "", "", "", ""]);
    }else {
        replace([]);
    }
}

const MaxRule = number().integer().required().max(100).test('Max_Number', 'Max Number must greater than Min Number', (value) => {
                        return value > inputValueRef.value[0]?.inputValueRef.value ? true: false
                });

const MinRule = number().integer().required().max(100).test('Min_Number', 'Min Number must less than Max Number', (value) => {
                        return value < inputValueRef.value[1]?.inputValueRef.value ? true: false 
                });


const onInput = (e) => {

    for(let i = 0; i < inputValueRef.value.length; i++){
        inputValueRef.value[i].handleChange(inputValueRef.value[i].inputValueRef.value);
    }

};

onMounted(() => {
    currentToggle.value = fieldCodeType.value;

    if(currentToggle.value == 0 && fields.value?.length == 0) {
        replace([null, null]);
    }
    if(currentToggle.value == 1 && fields.value?.length == 0) {
        replace(["", "", ""]);
    }
    if(currentToggle.value == 2 && fields.value?.length == 0){
        replace(["", "", "", "", ""]);
    }
});

</script>

<style scoped></style>