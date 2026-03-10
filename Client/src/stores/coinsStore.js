import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed } from 'vue'
import { get } from '@vueuse/core';

export const coinsStore = defineStore('coinsStore', () => {

    const coinsHistory = ref([]);
    const hasMore = ref(true);
    const countCoinsHistory = computed(() => coinsHistory.value.length);
    const coinsGraph = ref();

    const fetchCoinsHistory = async (startupid, type, pageNumber, pageSize) => {
        const params = {
            startupId: startupid,
            type: type,
            pageNumber: pageNumber,
            pageSize: pageSize
        }
        const request = await axios.get('/api/coinhistory/GetCoinHistoryForStartup', { params } );
        coinsHistory.value = Array.isArray(request.data?.data) ? [...coinsHistory.value, ...request.data?.data] : [...coinsHistory.value];
        hasMore.value = request.data?.hasMore ? request.data?.hasMore : false;
    };

    const fetchCoinsGraph = async (startupid, time, type) => {
        const params = {
            startupId: startupid,
            time: time,
            type: type
        }
        const {data, status} = await axios.get(`/api/CoinHistory/GetGraph`, {
            params
        });
        if(status == 200){
            coinsGraph.value = data;
        }
    };

    return { coinsHistory, fetchCoinsHistory, hasMore, countCoinsHistory, fetchCoinsGraph, coinsGraph};
});