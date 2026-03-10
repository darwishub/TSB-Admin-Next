import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { readFileSync } from 'fs'
import { join, resolve } from 'path';
import { fileURLToPath, URL } from 'url'

const baseFolder =
    process.env.APPDATA !== undefined && process.env.APPDATA !== ''
        ? `${process.env.APPDATA}/ASP.NET/https`
        : `${process.env.HOME}/.aspnet/https`;

const certificateName = process.env.npm_package_name

const certFilePath = join(baseFolder, `${certificateName}.pem`);
const keyFilePath = join(baseFolder, `${certificateName}.key`);

// https://vitejs.dev/config/
export default defineConfig({
    build: {
        rollupOptions: {
            output: {
                'home': './src/views/dashboard/Home',
                'quest': './src/views/dashboard/Quest',
                'quest-details': './src/views/dashboard/QuestDetails',
                'confirm': './src/views/email_confirmation/Confirm',
                'confirm-expired': './src/views/email_confirmation/ConfirmExpired',
                'confirm-resend': './src/views/email_confirmation/ConfirmResend',
                'confirm-successful': './src/views/email_confirmation/ConfirmSuccessful',
                'forgot': './src/views/email_reset_password/Forgot',
                'reset': './src/views/email_reset_password/Reset',
                'reset-expired': './src/views/email_reset_password/ResetExpired',
                'reset-successful': './src/views/email_reset_password/ResetSuccessful',
                'linkedin-confirm': './src/views/linkedin_confirmation/LinkedInConfirm',
                'dashboard-layout': './src/views/shared/Dashboard',
                'external-route-layout': './src/views/shared/ExternalRoute',
                'signin': './src/views/sign_in/Signin',
                'signup': './src/views/sign_up/Signup',
                'verify-email': './src/views/sign_up/VerifyEmail',
            },
        },
    },
    plugins: [vue(
        {
            template: {
                compilerOptions: {
                    // treat all tags with a ion-icon as custom elements
                    isCustomElement: (tag) => ['ion-icon'].includes(tag)
                }
            }
        }
    )],
    resolve: {
        alias: {
            "@": resolve(__dirname, "./src"),
        }
    },
    server: {
        https: {
            key: readFileSync(keyFilePath),
            cert: readFileSync(certFilePath)
        },
        proxy: {
            '/api/': {
                target: 'https://localhost:5001/',
                changeOrigin: true,
                secure: false,
                ws: true,
            }
        }
    }
})