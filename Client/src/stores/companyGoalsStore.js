import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'

export const useCompanyGoalsStore = defineStore('companyGoalsStore', () => {
    
    const companyGoal = ref();
    const userOnboarding = ref();
    let personalGraph = ref();

    const fetchCompanyGoal = async (startupid, programId) => {
        const { data, status } = await axios.get(`/api/startup/GetCompanyDetails?startupid=${startupid}&programId=${programId}`);
        if(status == 200) {
            companyGoal.value = data;
        }
    };

    const fetchUserOnboardingData = async () => {
        const { data, status } = await axios.get('/api/dashboard/UserOnBoarding');
        if(status == 200) {
            userOnboarding.value = data;
        }
    };

    const submitOnboarding = async (formData) => {
        return await axios.post('/api/dashboard/SubmitOnBoarding', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    };

    const finishOnboarding = async () => {
        return await axios.post('/api/dashboard/SubmitCollect');
    };

    const fetchBMC = async (startupid) => {
        try {
            const params = {
                startupid: startupid
            }
            const response = await axios.get('/api/Startup/GetBMCbyStartupId', { params });
            return response;
        } catch (error) {
            console.error(error);
        }
    };

    const fetchVPC = async (startupid) => {
        try {
            const params = {
                startupid: startupid
            }
            const response = await axios.get('/api/Startup/GetVPCStartupId', { params });
            return response;
        } catch (error) {
            console.error(error);
        }
    };

    const fetchPersonalGraph = async (programId, startupid, time) => {
        const { data, status } = await axios.get(`/api/Startup/graphProfile?programid=${programId}&startupid=${startupid}&time=${time}`);
        if(status == 200) {
            personalGraph.value = data;
        }

    };

    const startupNote = ref('');

    const fetchNote = async (startupid) => {
        try {
            const { data } = await axios.get(`/api/Startup/GetStartupNote?startupid=${startupid}`);
            startupNote.value = data.note ?? '';
        } catch (error) {
            console.error(error);
        }
    };

    const saveNote = async (startupid, note) => {
        return await axios.post(`/api/Startup/SaveStartupNote?startupid=${startupid}`, { Note: note });
    };

    return { companyGoal, userOnboarding, fetchCompanyGoal, fetchUserOnboardingData, submitOnboarding, finishOnboarding, fetchBMC, fetchVPC,
        personalGraph, fetchPersonalGraph, startupNote, fetchNote, saveNote };
});