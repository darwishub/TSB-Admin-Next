import { defineStore } from 'pinia'
import axios from 'axios'
import { ref, computed } from 'vue'

export const useShopStore = defineStore('useShopStore', () => {
    
    const shopEntryPreview = ref({
        price_type: 1,
        price: 0
    });
    const shopItems = ref();
    const itemDetail = ref();

    const fetchUserOnboardingData = async () => {
        const { data, status } = await axios.get('/api/dashboard/UserOnBoarding');
    };

    const fetchAllShopItems = async (Search, ItemType, ProgramId, ShowEntries, Pagination, ProgramGroupId, ALL) => {
        const header = { params: {
            Search: Search,
            ItemType: ItemType,
            ProgramId: ProgramId,
            ShowEntries: ShowEntries,
            Pagination: Pagination,
            ProgramGroupId: ProgramGroupId,
            ALL: ALL
        } };
        const { data, status } = await axios.get('/api/shop/AllShopItems',  header);
        if(status == 200){
            shopItems.value = data;
        }
    };

    const SubmitShopEntry = async (formData) => {
        return await axios.post('/api/shop/SubmitShopEntry', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    };

    const SubmitEditShopEntry = async (formData) => {
        return await axios.post('/api/shop/SubmitEditShopEntry', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    };

    const DeleteItem = async (content_type, id) => {
        const { status, data } = await axios.post(`api/shop/DeleteItem?content_type=${content_type}&id=${id}`);
        if(status == 200){
            shopItems.value = data;
        }
    }

    const GetShopItem = async (content_type, id) => {
        const {data, status} = await axios.get(`/api/shop/ShopItemDetail?content_type=${content_type}&id=${id}`);
        if(status == 200){
            itemDetail.value = data;
        }
    }

    const shopPreviewRestart = () => {
        shopEntryPreview.value = {
            price_type: 1,
            price: 0
        };
    }

    const SaveShopEntry = async (formData) => {
        return await axios.post('/api/shop/SaveShopEntry', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    };

    const SaveEditShopEntry = async (formData) => {
        return await axios.post('/api/shop/SaveEditShopEntry', formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
    };

    return { fetchUserOnboardingData, GetShopItem, SubmitEditShopEntry, SubmitShopEntry, 
        DeleteItem, shopItems, shopEntryPreview, itemDetail, shopPreviewRestart, fetchAllShopItems,
        SaveShopEntry, SaveEditShopEntry };
},
{
    persist: {
        storage: localStorage,
    }
});