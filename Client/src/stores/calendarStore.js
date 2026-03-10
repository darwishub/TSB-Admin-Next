import { defineStore } from 'pinia'
import axios from 'axios'
import { ref } from 'vue'

export const calendarStore = defineStore('useCalendarStore', () => {
    let calendarList = ref();
    let calendarLogs = ref();

    const fetchCalendarList = async (programId) => {
        const {data, status} = await axios.get(`/api/schedules/GetListCalendars?programId=${programId}`);
        if(status == 200){
            calendarList.value = data;
        }
    };

    const fetchCalendarLogs = async () => {
        const {data, status} = await axios.get(`/api/schedules/GetLogsCalendar`);
        if(status == 200){
            calendarLogs.value = data;
        }
    };

    return { fetchCalendarList, calendarList, fetchCalendarLogs, calendarLogs}

});