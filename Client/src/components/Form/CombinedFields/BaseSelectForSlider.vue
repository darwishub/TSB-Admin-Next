<template>
    <div>
        <BaseSelect 
            :name="props.name" 
            :options="options" 
            :label="props.label" 
            :placeholder="props.placeholder"   
            :is-required="props.isRequired" 
            @onSelect="onSelect"
            ref="selectRef"  
            v-bind="{
                'value-by': options !== undefined ? valueBy : null,
                'label-by': options !== undefined ? labelBy : null,
            }"
            v-if="options !== undefined"
            :value="fieldCodeType"
        />
        <template v-if="currentToggle == 0">
            <BaseInput :name="props.name_type_rule" type="hidden" class="d-none" value="number" />
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
        </template>
        
        <template v-if="currentToggle == 1">
            <BaseInput :name="props.name_type_rule" type="hidden" class="d-none" value="text" />
            <div v-for="(field, idx) in fields" :key="field.key">
                <BaseInput :name="`${props.name_option}[${idx}]`" :label="`Choice #${idx + 1}/${fields.length}`" placeholder="Enter slider choice" type="text" :is-required="true" />
            </div>
        </template>
        <template v-if="currentToggle == 2">
            <BaseInput :name="props.name_type_rule" type="hidden" class="d-none" value="text" />
            <div v-for="(field, idx) in fields" :key="field.key">
                <BaseInput :name="`${props.name_option}[${idx}]`" :label="`Choice #${idx + 1}/${fields.length}`" placeholder="Enter slider choice" type="text" :is-required="true" />
            </div>
        </template>
    </div>
</template>

<script setup>
import BaseSelect from "@/components/Form/BaseSelect.vue"
import BaseInput from "@/components/Form/BaseInput.vue"
import { ref, onMounted, computed, watch, toRefs } from "vue"
import { useFieldArray } from 'vee-validate';
import { number } from "yup"
import { useRoute } from "vue-router";

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
    name_option: {
        type: String
    },
    name_type_rule: {
        type: String
    },
    fieldCodeType: {
        type: [Number, String]
    }
});
const currentToggle = ref(null);
const selectRef = ref();
const inputValueRef = ref([]);

const { options, valueBy, labelBy, fieldCodeType, name_option } = toRefs(props);
const { remove, push, replace, fields } = useFieldArray(name_option.value);

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

const getOptions = computed(() => props.options !== undefined ? props.options : []);
const route = useRoute();

onMounted(() => {
    currentToggle.value = fieldCodeType.value;

    if(currentToggle.value == 0 && fields.value.length == 0) {
        replace([null, null]);
    }
    if(currentToggle.value == 1 && fields.value.length == 0) {
        replace(["", "", ""]);
    }
    if(currentToggle.value == 2 && fields.value.length == 0){
        replace(["", "", "", "", ""]);
    }
});

// watch(currentToggle, (newValue) => {
//   if(newValue == 0){
//       currentToggle.value = 0
//   } else if(newValue == 1) {
//       currentToggle.value = 1
//   } else if(newValue == 2){
//      currentToggle.value = 2
//   }

// });


</script>
