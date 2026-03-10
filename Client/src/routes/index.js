import { createWebHistory, createRouter } from 'vue-router'

const Dashboard = () => import('@/views/shared/Dashboard.vue')
const ExternalRoute = () => import('@/views/shared/ExternalRoute.vue')
const Home = () => import('@/views/dashboard/Home.vue')
const Quests = () => import('@/views/dashboard/Quests.vue')
const Shop = () => import('@/views/dashboard/Shop.vue')
const ShopEntry = () => import('@/views/dashboard/ShopEntry.vue')
const EditShopEntry = () => import('@/views/dashboard/EditShopEntry.vue')
const EditQuest = () => import('@/views/dashboard/EditQuest.vue')
const PreviewQuest = () => import('@/views/dashboard/PreviewQuest.vue')
const Academy = () => import('@/views/dashboard/Academy.vue')
const AcademyEntry = () => import('@/views/dashboard/AcademyEntry.vue')
const EditAcademyEntry = () => import('@/views/dashboard/EditAcademyEntry.vue')
const Signin = () => import('@/views/sign_in/Signin.vue')
const Signup = () => import('@/views/sign_up/Signup.vue')
const VerifyEmail = () => import('@/views/sign_up/VerifyEmail.vue')
const Confirm = () => import('@/views/email_confirmation/Confirm.vue')
const ConfirmExpired = () => import('@/views/email_confirmation/ConfirmExpired.vue')
const ConfirmResend = () => import('@/views/email_confirmation/ConfirmResend.vue')
const ConfirmSuccessful = () => import('@/views/email_confirmation/ConfirmSuccessful.vue')
const Forgot = () => import('@/views/email_reset_password/Forgot.vue')
const Reset = () => import('@/views/email_reset_password/Reset.vue')
const ResetExpired = () => import('@/views/email_reset_password/ResetExpired.vue')
const ResetSuccessful = () => import('@/views/email_reset_password/ResetSuccessful.vue')
const LinkedInConfirm = () => import('@/views/linkedin_confirmation/LinkedInConfirm.vue')
const CompanyProfile = () => import('@/views/dashboard/CompanyProfile.vue')
const UserProfile = () => import('@/views/dashboard/UserProfile.vue')
const UserOnboarding = () => import('@/views/dashboard/UserOnboarding.vue')
const Settings = () => import('@/views/dashboard/Settings.vue')
const PreviewShopEntry = () => import('@/views/dashboard/ShopEntryPreview.vue')
const PreviewAcademyEntry = () => import('@/views/dashboard/AcademyEntryPreview.vue')
const Calendar = () => import('@/views/dashboard/Calendar.vue')
const CoinsHistory = () => import('@/views/dashboard/CoinsHistory.vue')

// import VueCookies from 'vue-cookies'
import { adminAuthStore } from "@/stores/adminAuthStore"


const routes = [
    {
        path: '/:pathMatch(.*)*',
        redirect: '/signin'
    },
    {
        path: '/',
        redirect: '/home'
    },
    {
        path: '/',
        name: 'Dashboard',
        component: Dashboard,
        children: [
            {
                path: '/home',
                name: 'Home',
                component: Home,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/dashboard'
                }
            },
            {
                path: '/quests',
                name: 'Quests',
                component: Quests,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/quests'
                }
            },
            {
                path: '/preview-quest',
                name: 'PreviewQuest',
                component: PreviewQuest,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/quest-preview'
                }
            },
            {
                path: '/edit-quest/:id',
                name: 'EditQuest',
                component: EditQuest,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/edit-quest'
                }
            },
            {
                path: '/company-profile/:id',
                name: 'CompanyProfile',
                component: CompanyProfile,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/company-profile'
                }
            },
            {
                path: '/user-profile/:userid/:startupid',
                name: 'UserProfile',
                component: UserProfile,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/user-profile'
                }
            },
            {
                path: '/shop',
                name: 'Shop',
                component: Shop,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/shop'
                }
            },
            {
                path: '/shop-entry',
                name: 'ShopEntry',
                component: ShopEntry,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/add-shop-entry'
                }
            },
            {
                path: '/shop-entry-edit/:name/:content_type/:id',
                name: 'EditShopEntry',
                component: EditShopEntry,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/edit-shop-entry'
                }
            },
            {
                path: '/preview-shop-entry',
                name: 'PreviewShopEntry',
                component: PreviewShopEntry,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/shop-entry-preview'
                }
            },
            {
                path: '/academy',
                name: 'Academy',
                component: Academy,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/academy'
                }
            },
            {
                path: '/academy-entry',
                name: 'AcademyEntry',
                component: AcademyEntry,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/add-academy-entry'
                }
            },
            {
                path: '/academy-entry-edit/:name/:id',
                name: 'EditAcademyEntry',
                component: EditAcademyEntry,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/edit-academy-entry'
                }
            },
            {
                path: '/preview-academy-entry',
                name: 'PreviewAcademyEntry',
                component: PreviewAcademyEntry,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/academy-entry-preview'
                }
            },
            {
                path: '/calendar',
                name: 'Calendar',
                component: Calendar,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/calendar'
                },
            },
            {
                path: '/coins-history/:id',
                name: 'CoinsHistory',
                component: CoinsHistory,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/coins-history'
                },
            },
            {
                path: '/signin',
                name: 'Signin',
                component: Signin
            },
            {
                path: '/signup',
                name: 'Signup',
                component: Signup
            },
            {
                path: '/verify-email',
                name: 'VerifyEmail',
                component: VerifyEmail
            },
            {
                path: '/confirm',
                name: 'Confirm',
                component: Confirm
            },
            {
                path: '/confirm-expired',
                name: 'ConfirmExpired',
                component: ConfirmExpired
            },
            {
                path: '/confirm-resend',
                name: 'ConfirmResend',
                component: ConfirmResend
            },
            {
                path: '/confirm-successful',
                name: 'ConfirmSuccessful',
                component: ConfirmSuccessful
            },
            {
                path: '/forgot',
                name: 'Forgot',
                component: Forgot
            },
            {
                path: '/reset',
                name: 'Reset',
                component: Reset
            },
            {
                path: '/reset-expired',
                name: 'ResetExpired',
                component: ResetExpired
            },
            {
                path: '/reset-successful',
                name: 'ResetSuccessful',
                component: ResetSuccessful
            },
            {
                path: '/linkedin-confirm',
                name: 'LinkedInConfirm',
                component: LinkedInConfirm
            },
            {
                path: '/user-onboarding',
                name: 'UserOnboarding',
                component: UserOnboarding,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/user-onboarding'
                }
            },
            {
                path: '/settings',
                name: 'Settings',
                component: Settings,
                meta: {
                    requiresAuth: true,
                    breadcrumb: '/settings'
                }
            },
        ]
    },
    {
        path: '/',
        name: 'ExternalRoute',
        component: ExternalRoute,
        children: [
            {
                path: '/signin',
                name: 'Signin',
                component: Signin
            },
        ]
    },


];

const router = createRouter({
    history: createWebHistory(),
    routes,
    scrollBehavior(to, from, savedPosition) {
        return { 
            top: 0,
            behavior: 'instant'
        }
    },
});

router.beforeEach((to, from, next) => {
    const loggedIn = adminAuthStore();
    if (to.name === 'Signin' && loggedIn.adminAuth.email) {
        next({ name: 'Home' })
    } else if (to.matched.some(record => record.meta.requiresAuth)) {
        if (!loggedIn.adminAuth.email) {
            next({
                path: '/signin',
            })
        } else {
            if(to.name === 'Home' && !loggedIn.adminAuth.onBoardingStatus ){
                router.push("/user-onboarding");
            }

            if(to.name === 'UserOnboarding' && loggedIn.adminAuth.onBoardingStatus ){
                router.push("/home");
            }
            
            next()
        }

    } else {
        next()
    }

});

export default router;