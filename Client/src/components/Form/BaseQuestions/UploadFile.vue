<template>
<div>
    <BaseInput :name="`${name}.schema.props.label`" label="Question" placeholder="Enter question title here" type="text"
        type-rule="text" class="mb-3" isRequired />
    <BaseInput :name="`${name}.schema.props.placeholder`" label="Question placeholder" placeholder="Enter placeholder here"
        type="text" type-rule="text" class="mb-3" isRequired />
    <BaseInput :name="`${name}.schema.props.text_button`" label="Upload Button Call to Action"
        placeholder="Enter button name here" type="text" type-rule="text" class="mb-3" isRequired />
    <BaseInput :name="`${name}.schema.props.max_data`" label="Max Files"
        placeholder="Enter the amount of max files that can be uploaded" type="text" type-rule="number"
        :rule-validation="number().nullable().required('this is a required field').min(1).max(5)" :is-required="true" />
    <div class="file-checkbox-wrapper">
        <BaseInputSwitch :name="`${name}.schema.props.allow_multiple`" label="Allow all media types" class="mb-3"
            ref="toggleRef" @onSwitchChange="onSwitchChange" />
        <div :class="{ 'div-disabled': isToggleOn || allowAll }">
            <BaseInputCheckbox v-for="(option, index) in fileTypes" :name="`${name}.schema.props.files_type[${index}]`"
                :value="option" type="checkbox" :id="index" :ref="(el) => (checkboxRef[index] = el)"
                :options="fileTypes" 
                />
        </div>
    </div>
    <BaseInputSwitch :name="`${name}.schema.props.required`" label="Required" />
    <!-- <pre>{{ fields }}</pre> -->
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
import { number, array, string } from "yup"
import { useFieldArray, useFieldValue } from 'vee-validate';

const props = defineProps({
    name: {
        type: String,
    },
    allowAll: {
        type: Boolean
    }
});

const { name, allowAll, filesType } = toRefs(props);
// const { remove, push, replace, fields } = useFieldArray(`${name.value}.schema.props.files_type`);

const checkboxRef = ref([]);
const toggleRef = ref(null);
const isToggleOn = ref(false);
const fileTypes = ["image/png", "image/jpeg", "application/pdf"];

const onSwitchChange = (e) => {
    isToggleOn.value = e;
    for (let i = 0; i < checkboxRef.value.length; i++) {
        if (!checkboxRef.value[i].checked) {
            checkboxRef.value[i].checkboxRef.checked = true;
            checkboxRef.value[i].handleChange(fileTypes[i]);
        }
    }
}

onMounted(() => {
    isToggleOn.value = allowAll.value;
    for (let i = 0; i < checkboxRef.value.length; i++) {
        if (checkboxRef.value[i].checked) {
            checkboxRef.value[i].checkboxRef.checked = true;
        }
    }
    // if(fields.value?.length == 0){
    //     replace([]);
    // }
});


</script>

<style scoped></style>