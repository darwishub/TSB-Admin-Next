<template>
    <div class="accordion shadow--lg" id="accordionExample">
        <div class="accordion-item" v-for="(accordion, index) in accordionObj" :key="accordion.title">
            <button 
                class="accordion-button px-5 pt-5 pb-4 text-bold" 
                :class="{ 'collapsed': !accordionObj[index].isExpanded }"
                type="button" 
                @click="handleAccordion(index)"
            >
                <div class="me-5 fs-48 accordion-number">{{ `0${index + 1}` }}</div>
                <div class="fs-32 accordion-title">{{ `${accordion.title}` }}</div>
            </button>
            <Collapse :when="accordionObj[index].isExpanded" class="v-collapse">
                <div 
                    class="accordion-collapse" 
                >
                    <div class="accordion-body d-flex">
                        <div class="me-5 fs-48 invisible text-bold">{{ `0${index + 1}` }}</div>
                        <div>{{ `${accordion.content}` }}</div>
                    </div>
                </div>
            </Collapse>
        </div>
    </div>
</template>

<script setup>
import { reactive } from "vue"
import { Collapse } from 'vue-collapsed'

const props = defineProps({
    accordionData: {
        type: Array
    }
});

const accordionObj = reactive(
	props.accordionData.map(({ title, content }, index) => ({
		title,
		content,
		isExpanded: index === 0, // Initial values, display expanded on mount
	}))
);

const handleAccordion = (selectedIndex) => {
    accordionObj.forEach((_, index) => {
    accordionObj[index].isExpanded = index === selectedIndex ? !accordionObj[index].isExpanded : false
  })
}

</script>

<style scoped>
.v-collapse {
    transition: height 300ms cubic-bezier(0.33, 1, 0.68, 1);
}

.accordion-button:not(.collapsed) {
    background: transparent;
    color: #302F2F;
    box-shadow: none;
}

.accordion-button:focus {
    border-color: transparent;
    box-shadow: none;
}

.accordion-item {
    border: none;
    border-bottom: 2px solid rgba(194, 194, 194, 0.5);
}

.accordion-body {
    padding: 0 3rem 3rem 3rem;
}

.accordion .accordion-number {
    color: #c2c2c2;
}

.accordion .accordion-title {
    color: #969696;
}

.accordion-button:not(.collapsed) .accordion-number {
    color: #0F934C;
}

.accordion-button:not(.collapsed) .accordion-title {
    color: #302F2F
}

.accordion-button:not(.collapsed)::after {
    background-image: url('/accordion-open.svg');
    transform: rotate(-45deg);
}

.accordion-button::after {
    width: 3rem;
    height: 3rem;
    background-size: contain;
    background-image: url('/accordion-close.svg');
}</style>