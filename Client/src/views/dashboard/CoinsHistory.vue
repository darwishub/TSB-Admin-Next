<template>
    <PageExplanation class="no-toggle goals-topbar" :logo="adminAuth?.photo"
        :breadcrumbs="`${adminAuth?.name} ${route?.meta?.breadcrumb}`">
    </PageExplanation>
    <div class="container">
        <div class="row">
            <div class="mt-5">
                <div class="d-flex justify-content-between align-items-start mb-5">
                    <div class="d-flex align-items-center">
                        <div class="flex-shrink-0">
                            <div class="shadow-out--sm p-3 rounded-md">
                                <Ionicon icon_name="calendar-clear-outline" font_size="32" class="info" />
                            </div>
                        </div>
                        <div class="flex-grow-1 ms-3">
                            <div class="d-flex justify-content-start align-items-center gap-3">
                                <div class="f-game fs-24 text-md text-info">ACTIVITY</div>
                                <div class="cursor-pointer" data-bs-toggle="dropdown" aria-expanded="false">
                                    <Ionicon icon_name="ellipsis-vertical-circle-sharp" class="grey" font_size="32" />
                                </div>
                                <ul class="dropdown-menu" style="min-width: auto;">
                                    <li v-for="(time, index) in timePeriod" @click="onTimePeriodSelect(time.value)">
                                        <span class="fs-14 cursor-pointer dropdown-item">{{ time.label }}</span>
                                    </li>
                                </ul>
                            </div>
                            <div class="fs-14 text-info text-md">{{ timePeriodName }}</div>
                        </div>
                    </div>
                </div>
                <LineChart :chartData="chartData" :options="options" />
            </div>
            <div class="my-5">
                <div class="fs-24 text-sm-bold mb-3">History Detail</div>
                <template v-for="(data, index) in coinsHistory" :key="data.id">
                    <div class="col-sm-12 col-lg-6 offset-lg-3 mb-3">
                        <div class="d-flex align-items-center radius-16 shadow--md p-3">
                            <div class="flex-shrink-0">
                                <Ionicon
                                    :icon_name="data.transactionType.includes('income') ? 'trending-up' : 'trending-down'"
                                    font_size="24"
                                    :class="data.transactionType.includes('income') ? 'green-2' : 'danger'" />
                            </div>
                            <div class="flex-grow-1 mx-3">
                                <p style="word-break: break-word;" class="fs-14 mb-0"><span
                                        class="fw-bold text-warning">{{ data.amount }}</span> {{ data.description }}</p>
                                <p class="mb-0 base-grey"><small class="text-body-secondary fs-12">{{
                                    formatTimeAgo(convertedTime(data.transactionDate)) }}</small></p>
                            </div>
                        </div>
                    </div>
                </template>

                <Button class="green outline mx-auto d-table rounded-md mt-5" type="button" @click="onViewMore"
                    :loading="loading" :disabled="loading" v-if="hasMore">
                    <div class="d-flex justify-content-center align-items-center">
                        <div class="f-game fs-24">View More</div>
                    </div>
                </Button>
                <p class="text-center f-game fs-24 base-grey mt-3" v-if="coinsHistory.length == 0">No coins history
                    available</p>
            </div>
        </div>
    </div>
</template>
<script setup>
import PageExplanation from "@/components/PageExplanation.vue"
import Ionicon from "@/components/Ionicon.vue";
import { storeToRefs } from 'pinia';
import { coinsStore } from "@/stores/coinsStore"
import { computed, onMounted, ref, watch, reactive } from "vue"
import Button from "@/components/Button.vue"
import { formatTimeAgo } from '@vueuse/core'
import { LineChart, BarChart } from 'vue-chart-3';
import { Chart, registerables } from "chart.js";
import { useListCompanyStore } from "@/stores/listCompanyStore"
import { adminAuthStore } from "@/stores/adminAuthStore"
import { useRouter, useRoute, onBeforeRouteLeave } from "vue-router";


const { adminAuth } = storeToRefs(adminAuthStore());
const { listCompanies, graph } = storeToRefs(useListCompanyStore());
const { fetchListCompany, fetchGraph } = useListCompanyStore();

Chart.register(...registerables);
const { coinsHistory, hasMore, coinsGraph, countCoinsHistory } = storeToRefs(coinsStore());
const { fetchCoinsHistory, fetchCoinsGraph } = coinsStore();
const route = useRoute();
const timePeriodName = ref("Last 7 Days");
const timePeriod = [
    {
        "label": "Last 7 Days",
        "value": "weekly"
    },
    {
        "label": "Last 30 Days",
        "value": "monthly"
    },
    {
        "label": "Last 3 Months",
        "value": "last3months"
    },
    {
        "label": "Last 12 Months",
        "value": "yearly"
    }

];

let timePeriodSelected = ref("weekly");
const onTimePeriodSelect = async (selected) => {
    timePeriodSelected.value = selected;
    await fetchCoinsGraph(route.params.id, timePeriodSelected.value, "");
    chartData.labels = coinsGraph.value?.labels;
    chartData.datasets[0].data = coinsGraph.value?.income;
    chartData.datasets[1].data = coinsGraph.value?.outcome;

    timePeriodName.value = timePeriod.find((time) => time.value === selected).label;
}



const viewMoreNumber = ref(1);
const loading = ref(false);
const convertedTime = (utcTime) => {
    const localDate = new Date(utcTime + "Z");
    return localDate;
}
const onViewMore = async () => {
    viewMoreNumber.value += 1;
    loading.value = true;
    await fetchCoinsHistory(route.params.id, "", viewMoreNumber.value, 10);
    loading.value = false;
}


async function fetchData(id) {
    loading.value = true;
    await fetchCoinsHistory(id, "", viewMoreNumber.value, 10);
    loading.value = false;

    await fetchCoinsGraph(id, timePeriodSelected.value, "");

    chartData.labels = coinsGraph.value?.labels;
    chartData.datasets[0].data = coinsGraph.value?.income;
    chartData.datasets[1].data = coinsGraph.value?.outcome;
}

onMounted(async () => {
    coinsHistory.value = [];
    await fetchData(route.params.id);
});

watch(() => route.params.id, async (newId, oldId) => {
    if (newId !== oldId) {
        await fetchData(newId);
    }
});

let chartData = reactive({
    labels: [],
    datasets: [
        {
            label: 'Income',
            data: [0, 0, 0, 0, 0, 0, 0],
            fill: false,
            borderColor: '#0F934C',
            borderWidth: 4,
            tension: 0.4,
        },
        {
            label: 'Outcome',
            data: [0, 0, 0, 0, 0, 0, 0],
            fill: false,
            borderColor: '#BF2E3C',
            borderWidth: 4,
            tension: 0.4,
        }
    ],
});

const options = {
    scales: {
        yAxis: {
            title: {
                display: true,
                text: 'Coins',
            },
        },
    },
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
        tooltip: {
            displayColors: false,
            callbacks: {
                label: function (context) {
                    return context.formattedValue + ' steps done';
                },
            },
            backgroundColor: "#416094",
            titleColor: "#fff",
            titleFont: {
                family: "'Poppins', sans-serif",
                size: 12,
                weight: '500'
            },
            bodyColor: "#fff",
            bodyFont: {
                family: "'Poppins', sans-serif",
                size: 12,
                weight: '500'
            },
            footerColor: "#fff",
            textDirection: 'ltr',
        },
    },
};


</script>