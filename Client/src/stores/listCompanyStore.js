import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed, reactive } from 'vue'

export const useListCompanyStore = defineStore('listCompany', () => {
    const listCompanies = ref([]);
    let graph = ref();

    const fetchListCompany = async (search, sortByRank, sortByCoins, ShowEntries, Pagination, programId, ProgramGroupId, ALL) => {
        const { data, status } = await axios.get(`api/Startup/GetAllStartups?search=${search}&sortByRank=${sortByRank}&sortByCoins=${sortByCoins}&ShowEntries=${ShowEntries}&Pagination=${Pagination}&ProgramId=${programId}&ProgramGroupId=${ProgramGroupId}&ALL=${ALL}`);
        if(status == 200){
            listCompanies.value = data;
        }
    };

    const fetchGraph = async (programId, time) => {
        const { data, status } = await axios.get(`api/Dashboard/GetGraph?ProgramId=${programId}&time=${time}`);

        if(status == 200) {
            graph.value = data;
        }
        
    };

    const removeProgram = async (startupId) => {
        return await axios.post(`api/Startup/DeleteFromProgram?startupId=${startupId}`);
    };

    const switchProgram = async (startupId, programId, programGroupId) => {
        return await axios.post(`api/Startup/SelectStartupForProgram?startupId=${startupId}&programId=${programId}&programGroupId=${programGroupId}`);
    };

    return { listCompanies, graph, fetchListCompany, fetchGraph, removeProgram, switchProgram };
});