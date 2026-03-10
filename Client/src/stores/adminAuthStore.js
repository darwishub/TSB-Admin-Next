import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed, reactive } from 'vue'

export const adminAuthStore = defineStore('adminAuth', () => {

    const adminAuth = reactive({
        email: "",
        coins: "",
        onBoardingStatus: false,
        photo: null,
        role: "",
        programId: 0,
        programGroupId: 0,
        name: ""
    });

    const programGroup = ref([]);
    const programList = ref([]);
    const startupInfo = ref();

    const currentProgram = computed(() => {
        return programList.value?.find((element, index) => {
            return element.Programid == adminAuth.programId;
        });
    });

    const currentProgramGroup = computed(() => {
        return programGroup.value?.find((element, index) => {
            return element.programGroupId == adminAuth.programGroupId;
        });
    });

    const programs = computed(() => {
        if(programList.value?.length > 0){
            return programList.value?.filter(x => x.Programid != 0);
        }
    });

    const fetchProgram = async () => {
        const { data } = await axios.post(`/api/auth/FetchProgram`);
        programList.value = data;
        programList.value.push({
            Code : "",
            Enddate : "",
            Limit: 0,
            Name: "ALL Programs",
            Permission: true,
            ProgramType: null,
            Programid: 0,
            Startdate: ""
        });
    };

    const fetchUsersData = async () => {
        const { data, status } = await axios.get(`/api/Auth/GetUsersData`);
        if (status == 200) {
            adminAuth.email = data.email;
            adminAuth.coins = data.coins;
            adminAuth.onBoardingStatus = data.onBoardingStatus;
            adminAuth.role = data.role;
            adminAuth.programId = data.programId;
            adminAuth.name = data.name;
            adminAuth.photo = data.photo
        }
    };

    const fetchStartupInfo = async (id) => {
        const { status ,data } = await axios.get(`/api/startup/GetStartupInfo?startupid=${id}`);
        if (status == 200) {
            startupInfo.value = data;
        }
    }

    const fetchProgramGroup = async (programId) => {
        const { data } = await axios.get(`/api/Goals/GetProgramGroupByProgramId?programId=${programId}`);
        programGroup.value = data;
    }

    return { adminAuth, programList, fetchProgram, currentProgram, fetchUsersData, programs, fetchStartupInfo, startupInfo,
        fetchProgramGroup, programGroup, currentProgramGroup };
},
{
    persist: {
        storage: sessionStorage,
    }
});