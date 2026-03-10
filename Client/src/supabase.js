import { createClient } from '@supabase/supabase-js'

const supabaseUrl = 'https://npxallbspxbbeegjxtox.supabase.co';
const supabaseAnonKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im5weGFsbGJzcHhiYmVlZ2p4dG94Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2NTU4Mjc0MzYsImV4cCI6MTk3MTQwMzQzNn0.BOxY4elNMg_Kh3--h5Ssb0_BiSxxUSdwekxzLBKC-Ts';

export const supabase = createClient(supabaseUrl, supabaseAnonKey)