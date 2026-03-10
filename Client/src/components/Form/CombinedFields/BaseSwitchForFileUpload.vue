<template>
    <div>
        <BaseInput :name="props.name_max" label="Max Files" placeholder="Enter the amount of max files that can be uploaded"
            type="text" type-rule="number"
            :rule-validation="number().nullable().required('this is a required field').max(5)" :is-required="true" />
        <div class="file-checkbox-wrapper">
            <BaseInputSwitch :name="props.name_switch" label="Allow all media types" class="mb-3" ref="toggleRef"
                @onSwitchChange="onSwitchChange" />
            <div :class="{ 'div-disabled' : isToggleOn ||  allowAll}">
                <BaseInputCheckbox v-for="(option, index) in fileTypes" :name="props.name_options" :value="option"
                    type="checkbox" :is-required="true" :id="index" :ref="(el) => (checkboxRef[index] = el)"
                    :options="fileTypes" />
            </div>
        </div>
    </div>
</template>

<script setup>
import BaseInputSwitch from '@/components/Form/BaseInputSwitch.vue';
import BaseInputCheckbox from '@/components/Form/BaseInputCheckbox.vue';
import BaseInput from '@/components/Form/BaseInput.vue';
import { number, array, string } from "yup"

import { ref, onMounted, toRefs } from "vue"

const props = defineProps({
    name_switch: {
        type: String
    },
    name_max: {
        type: String
    },
    name_options: {
        type: String
    },
    id: {
        type: String
    },
    filesTypes: { type: Array }, 
    allowAll: { type: Boolean }
});
const {allowAll, filesTypes} = toRefs(props);
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
    isToggleOn.value = toggleRef.value.toggleRef.checked;
    for (let i = 0; i < checkboxRef.value.length; i++) {

        if (checkboxRef.value[i].checked) {
            checkboxRef.value[i].checkboxRef.checked = true;
        }
    }
});


</script>