import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed } from 'vue'

export const useAcademyStore = defineStore('useAcademyStore', () => {

    const academyEntryPreview = ref({
        price_type: 1,
        price: 0
    });
    const academyItems = ref();
    const itemDetail = ref();

    const fetchUserOnboardingData = async () => {
        try {
            const { data, status } = await axios.get('/api/dashboard/UserOnBoarding');
        }
        catch (error) {
            console.error(error);
        }
    };

    const fetchAllAcademyItems = async (ItemType, ProgramId, Search, ShowEntries, Pagination, programGroupId, ALL) => {
        try {
            const header = {
                params: {
                    ItemType: ItemType,
                    ProgramId: ProgramId,
                    Search: Search,
                    ShowEntries: ShowEntries,
                    Pagination: Pagination,
                    programGroupId: programGroupId,
                    ALL: ALL
                }
            };
            const { data, status } = await axios.get('/api/academy/AllAcademyItems', header);
            if (status == 200) {
                academyItems.value = data;
            }
        }
        catch (error) {
            console.log(error)
        }
    };

    const SubmitAcademyEntry = async (formData) => {
        return await axios.post('/api/academy/SubmitAcademyEntry', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    };

    const SubmitEditAcademyEntry = async (formData) => {
        return await axios.post('/api/academy/SubmitEditAcademyEntry', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    };

    const DeleteItem = async (id) => {
        const { status, data } = await axios.post(`api/academy/DeleteItem?id=${id}`);
        if (status == 200) {
            academyItems.value = data;
        }
    }

    const GetAcademyItem = async (id) => {
        const { data, status } = await axios.get(`/api/Academy/AcademyItemDetail?id=${id}`);
        if (status == 200) {
            itemDetail.value = data;
        }
    }

    const academyPreviewRestart = () => {
        academyEntryPreview.value = {
            price_type: 1,
            price: 0
        };
    }

    return { fetchUserOnboardingData, GetAcademyItem, SubmitEditAcademyEntry, SubmitAcademyEntry, DeleteItem, academyItems, academyEntryPreview, itemDetail, academyPreviewRestart, fetchAllAcademyItems };
},
    {
        persist: {
            storage: localStorage,
        }
    });