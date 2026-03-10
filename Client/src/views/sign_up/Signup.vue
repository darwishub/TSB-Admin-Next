<template>
  <div class="container">
    <div class="row">
      <div
        class="
          col-12 col-md-10
          offset-md-1
          col-lg-8
          offset-lg-2
          col-xl-6
          offset-xl-3
        "
      >
        <div class="steps-wrapper mt-6">
          <div class="row">
            <div
              class="col-4 d-flex justify-content-center"
              v-for="(step, index) in stepsObj"
              :key="index"
            >
              <div
                class="
                  d-md-inline-flex
                  align-items-center
                  steps-wrapper__step
                  user-select-none
                "
                :class="[
                  formPosition === index ? 'active-tab' : '',
                  stepValidations[index] ? 'is-valid-tab' : '',
                ]"
              >
                <div class="flex-shrink-0">
                  <Icon
                    :icon_name="step.icon_name"
                    :class="[
                      formPosition === index ? 'disabled' : 'disabled',
                      stepValidations[index] ? 'success' : '',
                    ]"
                    font_size="24"
                  />
                </div>
                <div class="flex-grow-1 ms-md-3">
                  <div class="steps-wrapper__step__info">
                    <div class="steps-wrapper__step__info__sub mt-2 mt-md-0">
                      {{ `Step ${step.step}/3` }}
                      <Ionicon
                        v-if="stepValidations[index]"
                        icon_name="checkmark-circle"
                        class="success"
                        font_size="16"
                        style="vertical-align: text-top"
                      />
                    </div>
                    <div
                      class="steps-wrapper__step__info__title d-none d-md-block"
                    >
                      {{ step.step_name }}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <Form :action="submitRegistration" autocomplete="off">
      <div class="content-wrapper mb-6">
        <div class="row content-fade-up-animation" v-show="formPosition === 0">
          <div class="col-12 col-md-8 col-lg-6 offset-md-2 offset-lg-3 mt-6">
            <div class="row">
              <div class="col-6 d-flex justify-content-center">
                <label>
                  <Card
                    class="px-3 py-2"
                    :class="[signUpModel.role == '0' ? 'green-1' : 'gray-0']"
                  >
                    <div class="form-check mb-3">
                      <InputRadio
                        class="form-check-input float-end"
                        value="0"
                        v-model="signUpModel.role"
                        name="role_type"
                        :checked="signUpModel.role == '0'"
                      />
                    </div>
                    <div class="mb-3">
                      <Icon
                        icon_name="rocket-sharp"
                        :class="[
                          signUpModel.role == '0' ? 'green-2' : 'disabled',
                        ]"
                        font_size="24"
                      />
                    </div>
                    <div class="d-block startup-info user-select-none">
                      <div class="text-sm-bold mb-2">Startup</div>
                      <div class="s-title">
                        Accelerate your company's growth using our tools,
                        resources and network.
                      </div>
                    </div>
                  </Card>
                </label>
              </div>
              <div class="col-6 d-flex justify-content-center">
                <label>
                  <Card
                    class="px-3 py-2"
                    :class="signUpModel.role == '1' ? 'green-1' : 'gray-0'"
                  >
                    <div class="form-check mb-3">
                      <InputRadio
                        class="form-check-input float-end"
                        value="1"
                        v-model="signUpModel.role"
                        name="role_type"
                        :checked="signUpModel.role == '1'"
                      />
                    </div>
                    <div class="mb-3">
                      <Icon
                        icon_name="people-circle-sharp"
                        :class="[
                          signUpModel.role == '1' ? 'green-2' : 'disabled',
                        ]"
                        font_size="24"
                      />
                    </div>
                    <div class="d-block investor-info user-select-none">
                      <div class="text-sm-bold mb-2">Investor</div>
                      <div class="s-title">
                        Find startups to support and invest in
                      </div>
                    </div>
                  </Card>
                </label>
              </div>
            </div>
          </div>
        </div>
        <div class="row content-fade-up-animation" v-show="formPosition === 1">
          <div class="d-flex justify-content-center">
            <div class="membership-type my-c-2">
              <div v-if="signUpModel.role == '0'">
                <div class="form-check form-check-inline">
                  <InputRadio
                    id="subscription-option"
                    class="form-check-input"
                    value="0"
                    v-model="signUpModel.payment_model"
                    name="payment_model"
                    :checked="signUpModel.payment_model == '0'"
                    @update:modelValue="checkPaymentModelValue"
                  />
                  <label class="form-check-label" for="subscription-option"
                    >Subscription</label
                  >
                </div>
                <div class="form-check form-check-inline">
                  <InputRadio
                    id="program-option"
                    class="form-check-input"
                    value="1"
                    v-model="signUpModel.payment_model"
                    name="payment_model"
                    :checked="signUpModel.payment_model == '1'"
                    @update:modelValue="checkPaymentModelValue"
                  />
                  <label class="form-check-label" for="program-option"
                    >Startup program</label
                  >
                </div>
              </div>
              <div v-else-if="signUpModel.role == '1'"></div>
            </div>
          </div>

          <div
            class="
              col-12 col-md-10 col-lg-10 col-xl-8
              offset-md-1 offset-lg-1 offset-xl-2
            "
          >
            <Transition name="payment-model">
              <div
                class="subscription-option-content"
                v-if="
                  (signUpModel.payment_model == '0' &&
                    signUpModel.role == '0') ||
                  (signUpModel.payment_model == '0' &&
                    signUpModel.role == '1') ||
                  (signUpModel.payment_model == '1' && signUpModel.role == '1')
                "
              >
                <div
                  slot-scope=""
                  class="form-check form-switch form-switch-multi-label mb-2"
                >
                  <label class="form-check-label" for="membership-duration"
                    >Monthly</label
                  >
                  <input
                    class="form-check-input"
                    type="checkbox"
                    id="membership-duration"
                    v-model="signUpModel.membership_duration"
                    checked
                  />
                  <label class="form-check-label" for="membership-duration"
                    >Yearly
                    <span class="text-success text-bold">15% Off</span></label
                  >
                </div>

                <div class="row">
                  <div class="col-12 col-lg-8 mb-3 mb-lg-0">
                    <div class="row g-2">
                      <div
                        class="col-12 col-md-3"
                        v-for="(membership, index) in getMembershipObj
                          .slice()
                          .reverse()"
                        :key="index"
                      >
                        <label class="membership-label-wrapper">
                          <Card
                            class="px-3 py-2"
                            :class="[
                              signUpModel.membership_type ==
                              membership.Membership_Id
                                ? 'green-1'
                                : 'gray-0',
                              membership.Membership_Id == '4'
                                ? 'best-value'
                                : '',
                            ]"
                          >
                            <div class="form-check mb-2">
                              <InputRadio
                                class="form-check-input float-end"
                                :value="membership.Membership_Id"
                                v-model="signUpModel.membership_type"
                                name="membership_type"
                                @update:modelValue="checkMembershipValue"
                                :checked="
                                  signUpModel.membership_type ==
                                  membership.Membership_Id
                                "
                              />
                            </div>
                            <div
                              class="
                                membership
                                d-flex
                                justify-content-between
                                d-md-block
                              "
                            >
                              <div class="membership__name">
                                {{ membership.Membership_Name }}
                              </div>
                              <div class="membership__price my-md-2">
                                $
                                {{
                                  membership.Membership_Price[
                                    signUpModel.membership_duration ? 1 : 0
                                  ]
                                }}
                              </div>
                              <div
                                class="membership__duration d-none d-md-block"
                              >
                                */{{
                                  signUpModel.membership_duration
                                    ? "Year"
                                    : "Month"
                                }}
                              </div>
                            </div>
                          </Card>
                        </label>
                      </div>
                    </div>
                    <div
                      class="
                        d-flex
                        justify-content-between
                        mt-2
                        membership-info
                      "
                    >
                      <div class="membership-info__text-info">
                        <a
                          :href="urlPricingDetails"
                          class="text-sm-bold"
                          target="_blank"
                          >Pricing details</a
                        >
                      </div>
                    </div>
                    <Transition name="slide-up">
                      <div v-if="signUpModel.membership_type != '1'">
                        <div class="payment-options my-c-2">
                          <Pills>
                            <template #button_pills>
                              <PillButton
                                class="mb-c-2"
                                target="option-1"
                                is_active
                                >Card
                                <Ionicon icon_name="card" font_size="24" />
                              </PillButton>
                              <!-- <PillButton target="option-2"
                                  >Google Pay<Ionicon
                                    icon_name="logo-google"
                                    font_size="24"
                                /></PillButton> -->
                            </template>
                            <template #content_pills>
                              <PillContent target="option-1" is_active is_show>
                                <StripeElements
                                  v-if="stripeLoaded"
                                  v-slot="{ elements, instance }"
                                  ref="elms"
                                  :stripe-key="stripeKey"
                                  :instance-options="instanceOptions"
                                  :elements-options="elementsOptions"
                                >
                                  <StripeElement
                                    ref="card"
                                    :elements="elements"
                                    :options="cardOptions"
                                    id="card"
                                    @change="checkCardValidity"
                                  />
                                </StripeElements>
                                <div class="text-danger stripe-error-msg">
                                  {{ cardErrorMessage }}
                                </div>
                              </PillContent>
                              <!-- <PillContent target="option-2"
                                  >tes 2 content</PillContent
                                > -->
                            </template>
                          </Pills>
                        </div>
                        <div class="promo mb-3">
                          <div class="form-floating">
                            <Input
                              placeholder="Promo code"
                              type="text"
                              name="promo_code"
                              v-model="signUpModel.promo_code"
                              :value="signUpModel.promo_code"
                              @update:modelValue="getPromoCode"
                              :class="[
                                signUpModel.promo_code != '' &&
                                signUpModel.promo_code != null
                                  ? isPromoCodeValid
                                    ? 'is-valid'
                                    : 'is-invalid'
                                  : '',
                              ]"
                            >
                              <label>Promo code</label>
                              <div class="invalid-feedback">
                                Hmm, that's an invalid code. Please try again
                              </div>
                              <div class="valid-feedback text-sm-bold">
                                {{
                                  PromoObj.length > 0
                                    ? "" + PromoObj[1] + " Applied!"
                                    : ""
                                }}
                              </div>
                            </Input>
                          </div>
                        </div>
                        <div class="skip-trial-wrapper">
                          <div class="form-check d-inline-block me-2">
                            <input
                              id="skip-trial"
                              class="form-check-input"
                              type="checkbox"
                              v-model="signUpModel.skip_trial"
                            />
                            <label
                              class="form-check-label user-select-none"
                              for="skip-trial"
                            >
                              Skip the 14 days trial
                            </label>
                          </div>
                          <Popover
                            content="By checking this field, you agree to skip the 14-days membership trial, and are entitled to access our premium features immediately."
                            class="d-inline-block"
                          >
                            <Ionicon
                              icon_name="help-circle"
                              class="info"
                              font_size="24"
                            />
                          </Popover>
                        </div>
                      </div>
                    </Transition>
                  </div>
                  <div class="col-12 col-lg-4">
                    <div class="features-list text-sm-bold">
                      What’s included :
                      <ul class="ps-4 lh-lg">
                        <li
                          v-for="(
                            feature, idx
                          ) in getSelectedMembershipObj.Membership_Features"
                          :key="idx"
                        >
                          {{ feature }}
                        </li>
                      </ul>
                    </div>

                    <div class="membership-overview my-4">
                      <ul class="list-group">
                        <li
                          class="
                            list-group-item
                            d-flex
                            justify-content-between
                            align-items-start
                          "
                        >
                          <div class="me-auto">
                            <div class="text-md">Membership type</div>
                            <span class="grey membership-overview__name">{{
                              getSelectedMembershipObj.Membership_Name
                            }}</span>
                          </div>
                          <span class="text-sm-bold"
                            >{{
                              signUpModel.membership_type == "1" ? "" : "$"
                            }}
                            {{
                              getSelectedMembershipObj.Membership_Price[
                                signUpModel.membership_duration ? 1 : 0
                              ]
                            }}</span
                          >
                        </li>
                        <li
                          v-if="signUpModel.membership_type != '1'"
                          class="
                            list-group-item
                            d-flex
                            justify-content-between
                            align-items-start
                          "
                        >
                          <div class="me-auto">
                            <div class="text-md">Promo code</div>
                            <span class="grey membership-overview__name">{{
                              PromoObj.length > 0 ? "" + PromoObj[1] + "" : ""
                            }}</span>
                          </div>
                          <span class="text-sm-bold text-danger">{{
                            PromoObj.length > 0
                              ? "- $ " + parseInt(PromoObj[0]) / 100 + ""
                              : ""
                          }}</span>
                        </li>
                        <li
                          class="
                            list-group-item
                            d-flex
                            justify-content-between
                            align-items-start
                          "
                        >
                          <div class="me-auto">
                            <div class="text-md">TOTAL</div>
                          </div>
                          <span class="text-sm-bold">{{
                            signUpModel.membership_type != "1"
                              ? `$ ${
                                  (
                                    getSelectedMembershipObj.Membership_Price[
                                      signUpModel.membership_duration ? 1 : 0
                                    ] -
                                    (PromoObj.length > 0
                                      ? parseInt(PromoObj[0]) / 100
                                      : 0)
                                  ).toFixed(2) < 0
                                    ? 0
                                    : (
                                        getSelectedMembershipObj
                                          .Membership_Price[
                                          signUpModel.membership_duration
                                            ? 1
                                            : 0
                                        ] -
                                        (PromoObj.length > 0
                                          ? parseInt(PromoObj[0]) / 100
                                          : 0)
                                      )
                                        .toFixed(2)
                                        .replace(/\.0+$/, "")
                                }`
                              : "FREE"
                          }}</span>
                        </li>
                      </ul>
                    </div>

                    <Transition name="slide-fade" mode="out-in">
                      <Testimony 
                        :key="testimonyObjIndex" 
                        :comment="testimonyObj[testimonyObjIndex].testimony"
                        :photo_url="testimonyObj[testimonyObjIndex].profile_url"
                        :name="testimonyObj[testimonyObjIndex].name"
                        :company="testimonyObj[testimonyObjIndex].company"
                      />
                    </Transition>
                  </div>
                </div>
              </div>
              <div
                class="program-option-content"
                v-else-if="
                  signUpModel.payment_model == '1' && signUpModel.role == '0'
                "
              >
                <div class="row">
                  <div class="col-12 col-lg-4 offset-lg-4">
                    <div class="form-floating">
                      <Input
                        placeholder="Program code"
                        type="text"
                        name="program_code"
                        :value="signUpModel.program_code"
                        v-model="signUpModel.program_code"
                        :class="[
                          signUpModel.program_code != '' &&
                          signUpModel.program_code != null
                            ? signUpModel.is_program_code_valid
                              ? 'is-valid'
                              : 'is-invalid'
                            : 'is-invalid',
                        ]"
                        @update:modelValue="getProgramCode"
                      >
                        <label>Program code</label>
                        <div class="invalid-feedback">
                          <div v-if="signUpModel.program_code.length == 0">
                            Insert a valid code to continue the process.
                          </div>
                          <div v-else>
                            Hmm, that's an invalid code. Please try again
                          </div>
                        </div>
                        <div class="valid-feedback text-sm-bold">
                          {{ "" + signUpModel.program_code + " Applied!" }}
                        </div>
                      </Input>
                    </div>
                  </div>
                </div>
              </div>
            </Transition>
          </div>
        </div>
        <div class="row content-fade-up-animation" v-show="formPosition === 2">
          <div class="col-12 col-sm-10 offset-sm-1 col-lg-4 offset-lg-4 mt-6">
            <div class="form-floating mb-3">
              <Input
                placeholder="Email"
                type="email"
                v-model="signUpModel.email"
                autocomplete="off"
                name="email"
                required
              >
                <label>Email</label>
              </Input>
            </div>

            <div class="form-floating mb-3">
              <Input
                placeholder="Password"
                v-on="{
                  input: checkStringPassword,
                  focus: resetErrorPassword,
                }"
                name="password"
                autocomplete="off"
                :type="passwordInputType"
                v-model="signUpModel.password"
                :class="{ 'is-invalid': !isSignupSuccess }"
                required
              >
                <label>Password</label>
                <div class="invalid-feedback">
                  {{ signupErrorMessage }}
                </div>
              </Input>
            </div>

            <div class="mb-3 signup-control">
              <div class="form-check signup-control__show-pass">
                <FormCheckBox
                  class="d-inline-block me-2"
                  id="show-password"
                  @change="showPassword"
                >
                  <label for="show-password" class="user-select-none"
                    >Show password</label
                  >
                </FormCheckBox>
              </div>
            </div>

            <div class="background double" id="signup-line">
              <span>OR CONNECT WITH</span>
            </div>

            <div class="button-sosmed mb-3">
              <div class="d-grid gap-3 d-flex justify-content-center">
                <Button
                  class="green outline"
                  type="button"
                  @click="GoogleAuth"
                  :disabled="isDisabled"
                  :loading="isGoogleLoading"
                  ><Ionicon icon_name="logo-google" font_size="24"
                /></Button>
                <Button
                  class="green outline"
                  type="button"
                  @click="FBAuth"
                  :disabled="isDisabled"
                  :loading="isFBLoading"
                  ><Ionicon icon_name="logo-facebook" font_size="24"
                /></Button>
                <Button
                  class="green outline"
                  type="button"
                  @click="LinkedInAuth"
                  :disabled="isDisabled"
                  :loading="isLinkedInLoading"
                  ><Ionicon icon_name="logo-linkedin" font_size="24"
                /></Button>
              </div>
              <div class="text-danger mt-3">{{ socialProviderError }}</div>
            </div>
            <div class="d-md-flex justify-content-center">
              <div class="tos-link">
                By signing up, you agree to our<a
                  href="https://www.thestartupbuddy.co/terms-and-conditions-policies"
                  target="_blank"
                >
                  Terms & Conditions</a
                >
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="d-grid gap-3 d-flex justify-content-center mb-c-2">
        <Button
          class="grey outline"
          type="button"
          v-if="formPosition > 0"
          @click="backStep"
          :disabled="isDisabled"
          ><Ionicon icon_name="chevron-back" font_size="24" /> Back</Button
        >
        <Button
          class="green"
          type="button"
          v-if="formPosition <= 1"
          @click="nextStep"
          >Next <Ionicon icon_name="chevron-forward" font_size="24"
        /></Button>
        <Button
          class="green"
          type="submit"
          :disabled="isDisabled"
          :loading="isLoading"
          :is_success="buttonStatus.isIdentitySuccess"
          :is_fail="buttonStatus.isIdentityFail"
          v-else
          >Confirm <Ionicon icon_name="chevron-forward" font_size="24"
        /></Button>
      </div>
    </Form>

    <div class="d-md-flex justify-content-md-center mb-6">
      <div>
        Already have an account?<router-link to="/signin">
          Sign in!</router-link
        >
      </div>
    </div>
  </div>
</template>

<script setup>
import Navbar from "@/components/Navbar.vue";
import Icon from "@/components/Icon.vue";
import Card from "@/components/Card.vue";
import Button from "@/components/Button.vue";
import Ionicon from "@/components/Ionicon.vue";
import Pills from "@/components/Nav/Pills/Pills.vue";
import PillButton from "@/components/Nav/Pills/PillButton.vue";
import PillContent from "@/components/Nav/Pills/PillContent.vue";
import Input from "@/components/Form/Input/Input.vue";
import FormCheckBox from "@/components/Form/FormCheck.vue";
import Popover from "@/components/Popover.vue";
import InputRadio from "@/components/Form/Input/InputRadio.vue";
import Form from "@/components/Form/Form.vue";
import Testimony from "@/components/Testimony.vue"
import { string, object, boolean } from "yup";

import { supabase } from "@/supabase";
import { loadStripe } from "@stripe/stripe-js";
import { useRouter } from "vue-router";
import { StripeElements, StripeElement } from "vue-stripe-js";
import { useGapi } from "vue-gapi";
import queryString from "query-string";
import {
  reactive,
  ref,
  onMounted,
  onBeforeMount,
  computed,
  inject,
  watchEffect,
} from "vue";

const axios = inject("axios");
const cookies = inject("$cookies");
const router = useRouter();
/*== Model ==*/
const signUpModel = reactive({
  role: "0",
  payment_model: "0",
  membership_duration: true,
  membership_type: "5",
  token: "",
  program_code: "",
  promo_code: "",
  is_program_code_valid: false,
  is_card_valid: false,
  skip_trial: false,
  email: "",
  password: "",
});

let testimonyObj = [
  {
    testimony:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor.",
    name: "Muhammad Darwis",
    company: "Macrosoft",
    profile_url:
      "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/profile-img-prod/2740Profile Pic.jpeg",
  },
  {
    testimony:
      "Sed nec condimentum sem. Pellentesque volutpat accumsan auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sed eleifend tellus.",
    name: "Johan Zarco",
    company: "Leopard Inc",
    profile_url:
      "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/profile-img-prod/274454343434.jpeg",
  },
  {
    testimony:
      "Curabitur sed eleifend tellus. Sed nec condimentum sem. Pellentesque volutpat accumsan auctor. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
    name: "Van Houten",
    company: "Adobe",
    profile_url:
      "https://s3.ap-southeast-1.amazonaws.com/www.thestartupbuddy.co/profile-img-prod/254519B81157-DBAC-4C67-9874-2EAF88ACF5BC_1_102_o.jpeg",
  },
];
let testimonyObjIndex = ref(0);
let passwordInputType = ref("password");
let formPosition = ref(0);
let isPromoCodeValid = ref(false);
let cardErrorMessage = ref("");
let PromoObj = ref([]);
let isSignupSuccess = ref(true);
let signupErrorMessage = ref("");
let programDisabled = ref(false);
let promoDisabled = ref(false);

let investorMembershipsType = [
  {
    Membership_Id: 1,
    Membership_Price: ["FREE", "FREE"],
    Membership_Name: "Angel",
    Membership_Features: [
      "Startup Stock Exchange Game",
      "Team Collaboration",
      "Join our Events",
      "Fund Builder",
    ],
  },
  {
    Membership_Id: 2,
    Membership_Price: ["49.99", "499"],
    Membership_Name: "VC",
    Membership_Features: ["Everything in Angel", "Receive startup matches"],
  },
  {
    Membership_Id: 4,
    Membership_Price: ["199", "1999"],
    Membership_Name: "Accelerator",
    Membership_Features: [
      "Everything in VC",
      "LP Sourcing (2% success fee)",
      "Monthly Seed Membership Benefits worth $50.000",
      "Invoice payment",
      "Startup Seed Program 50 pax",
      "Annual Seed Membership Benefits worth $150.000",
    ],
  },
  {
    Membership_Id: 5,
    Membership_Price: ["399", "3999"],
    Membership_Name: "Enterprise ",
    Membership_Features: [
      "Everything in Accelerator",
      "Startup Seed Program 250 pax",
      "Analytics and insights",
      "User training",
      "Dedicated support",
    ],
  },
];
let startupMembershipsType = [
  {
    Membership_Id: 1,
    Membership_Price: ["FREE", "FREE"],
    Membership_Name: "Ideation",
    Membership_Features: [
      "Pitch Builder",
      "Academy",
      "Proposition Generator",
      "Business Model Generator",
      "Dashboard",
    ],
  },
  {
    Membership_Id: 2,
    Membership_Price: ["9.99", "99.99"],
    Membership_Name: "Bootstrap",
    Membership_Features: [
      "Everything in Ideation",
      "Team Collaboration",
      "Startup Stock Exchange Trading Game",
      "Pitch Builder",
      "Join our Exclusive Events",
    ],
  },
  {
    Membership_Id: 4,
    Membership_Price: ["34.99", "349.99"],
    Membership_Name: "Seed Stage",
    Membership_Features: [
      "Everything in Boostrap",
      "Startup Stock Exchange Investor detail",
      "Monthly Starter Pack worth $7500",
      "Yearly Starter Pack Worth $150.000 (for annual members only)",
      "Networking",
    ],
  },
  {
    Membership_Id: 5,
    Membership_Price: ["99", "999"],
    Membership_Name: "Growth Stage",
    Membership_Features: [
      "Everything in Seed",
      "Personal Tech Support",
      "Personalise Investor Search (2% Success Fee)",
      "Yearly Starter Pack Worth $150000** (for annual members only)",
      "Invoice Payment",
    ],
  },
];

let getMembershipObj = computed(() => {
  if (signUpModel.role == "0") {
    return startupMembershipsType;
  } else if (signUpModel.role == "1") {
    return investorMembershipsType;
  }
});
let getSelectedMembershipObj = computed(() => {
  return getMembershipObj.value.find(
    (obj) => obj.Membership_Id == signUpModel.membership_type
  );
});

let stepsObj = reactive([
  {
    step: 1,
    step_name: "Role type",
    icon_name: "people",
    isValid: false,
  },
  {
    step: 2,
    step_name: "Membership",
    icon_name: "id-card",
    isValid: false,
  },
  {
    step: 3,
    step_name: "Confirmation",
    icon_name: "log-in",
    isValid: false,
  },
]);

let stepValidations = ref([false, false, false]);
const urlPricingDetails = computed(() => {
  return `https://www.thestartupbuddy.co/${
    signUpModel.role == 0 ? "startups" : "investor"
  }`;
});
const checkMembershipValue = () => {
  if (signUpModel.membership_type == "1") {
    signUpModel.promo_code = "";
    signUpModel.is_card_valid = false;
  }
};
const checkPaymentModelValue = () => {
  if (signUpModel.payment_model == "1") {
    signUpModel.promo_code = "";
    signUpModel.is_card_valid = false;
  } else {
    signUpModel.program_code = "";
    signUpModel.is_program_code_valid = false;
  }
};
const nextStep = () => {
  if (formPosition.value === 0) {
    if (signUpModel.role) {
      formPosition.value++;
      stepValidations.value[0] = true;
    }
    return false;
  }

  if (formPosition.value === 1) {
    //if role is startup
    if (signUpModel.role == "0") {
      //if payment model as subscription
      if (signUpModel.payment_model == "0") {
        //if membership type is higher than ideation
        if (signUpModel.membership_type != "1" && signUpModel.is_card_valid) {
          if (
            signUpModel.promo_code == "" ||
            (signUpModel.promo_code != "" && isPromoCodeValid.value)
          ) {
            formPosition.value++;
            stepValidations.value[1] = true;
          }
          return false;
        }
        //if memberhsip type is ideation
        else if (signUpModel.membership_type == "1") {
          formPosition.value++;
          stepValidations.value[1] = true;
        }

        cardErrorMessage.value =
          "Card number required, please insert a valid card number.";
        return false;
      }
      //if payment model as program
      else {
        if (
          (signUpModel.program_code != null &&
            signUpModel.is_program_code_valid) ||
          (signUpModel.program_code != "" && signUpModel.is_program_code_valid)
        ) {
          formPosition.value++;
          stepValidations.value[1] = true;
        }
        return false;
      }
    }
    //if role is investor
    if (signUpModel.role == "1") {
      if (
        signUpModel.payment_model == "0" ||
        signUpModel.payment_model == "1"
      ) {
        //if membership type is higher than private investor
        if (signUpModel.membership_type != "1" && signUpModel.is_card_valid) {
          if (
            signUpModel.promo_code == "" ||
            (signUpModel.promo_code != "" && isPromoCodeValid.value)
          ) {
            formPosition.value++;
            stepValidations.value[1] = true;
          }
          return false;
        }
        //if memberhsip type is private investor
        else if (signUpModel.membership_type == "1") {
          formPosition.value++;
          stepValidations.value[1] = true;
        }

        cardErrorMessage.value =
          "Card number required, please insert a valid card number.";
        return false;
      }
    }
  }
};

const backStep = () => {
  const cardElement = card.value ? card.value.stripeElement : null;

  if (formPosition.value === 2) {
    if (cardElement != null) {
      cardElement.clear();
    }
    signUpModel.is_card_valid = false;
    signUpModel.promo_code = "";
    signUpModel.program_code = "";
    signUpModel.is_program_code_valid = false;
    signUpModel.skip_trial = false;
    PromoObj.value = [];
    stepValidations.value[2] = false;
    stepValidations.value[1] = false;
    formPosition.value--;
  } else if (formPosition.value === 1) {
    if (cardElement != null) {
      cardElement.clear();
    }
    signUpModel.is_card_valid = false;
    signUpModel.promo_code = "";
    signUpModel.program_code = "";
    signUpModel.is_program_code_valid = false;
    signUpModel.skip_trial = false;
    PromoObj.value = [];
    stepValidations.value[1] = false;
    stepValidations.value[0] = false;
    formPosition.value--;
  }

  return false;
};

let showPassword = ($event) => {
  if ($event.target.checked === true) {
    passwordInputType.value = "text";
  } else {
    passwordInputType.value = "password";
  }
};

let isDisabled = ref(false);
let isLoading = ref(false);
let isLinkedInLoading = ref(false);
let isGoogleLoading = ref(false);
let isFBLoading = ref(false);
let isPwdDetailValid = ref(false);

const buttonStatus = reactive({
  isIdentitySuccess: false,
  isIdentityFail: false,
});

const resetErrorPassword = () => {
  signupErrorMessage.value = "";
  isSignupSuccess.value = true;
};

const checkStringPassword = (e) => {
  let value = e.target.value;
  if (value.length < 8) {
    signupErrorMessage.value = "Passwords must be at least 8 characters.";
    isSignupSuccess.value = false;
    isPwdDetailValid.value = false;
  } else {
    if (/[a-z]/.test(value)) {
      signupErrorMessage.value = "";
      isSignupSuccess.value = true;
      isPwdDetailValid.value = true;
    } else {
      signupErrorMessage.value =
        "Passwords must have at least one lowercase ('a'-'z').";
      isSignupSuccess.value = false;
      isPwdDetailValid.value = false;
    }
  }
};

const submitRegistration = () => {
  if (isPwdDetailValid.value) {
    isDisabled.value = true;
    isLoading.value = true;
    let convertDurationType = signUpModel.membership_duration ? 1 : 0;
    if (formPosition.value === 2) {
      if (signUpModel.email && signUpModel.password) {
        // stepValidations.value[2] = true;

        if (signUpModel.membership_type != "1" && signUpModel.is_card_valid) {
          const cardElement = card.value.stripeElement;
          elms.value.instance.createToken(cardElement).then((result) => {
            if (result != null) {
              const params = queryString.stringify({
                token: result.token.id,
                membership_type: parseInt(signUpModel.membership_type),
                coupon: signUpModel.promo_code,
                membership_duration: convertDurationType,
                email: signUpModel.email,
                password: signUpModel.password,
                programcode: signUpModel.program_code,
                skip_trial: signUpModel.skip_trial,
                role: signUpModel.role,
                payment_model: signUpModel.payment_model,
                card_valid: signUpModel.is_card_valid,
                program_valid: signUpModel.is_program_code_valid,
                address: location.origin,
              });

              axios
                .post(
                  "api/Auth/SignUp?" + params,
                  {},
                  { validateStatus: false }
                )
                .then((res) => {
                  if (res.status === 200) {
                    isSignupSuccess.value = true;
                    buttonStatus.isIdentitySuccess = true;
                    cookies.set("tsb-email", signUpModel.email);
                    router.push({
                      path: "/verifyemail",
                    });
                  } else {
                    isSignupSuccess.value = false;
                    buttonStatus.isIdentityFail = true;
                    signupErrorMessage.value = res.data;
                  }
                })
                .then(() => {
                  isLoading.value = false;
                })
                .finally(() => {
                  setTimeout(() => {
                    isDisabled.value = false;
                    buttonStatus.isIdentitySuccess = false;
                    buttonStatus.isIdentityFail = false;
                  }, 1000);
                });
            }
          });
        } else {
          const params = queryString.stringify({
            token: signUpModel.token,
            membership_type: parseInt(signUpModel.membership_type),
            coupon: signUpModel.promo_code,
            membership_duration: convertDurationType,
            email: signUpModel.email,
            password: signUpModel.password,
            programcode: signUpModel.program_code,
            skip_trial: signUpModel.skip_trial,
            role: signUpModel.role,
            payment_model: signUpModel.payment_model,
            card_valid: signUpModel.is_card_valid,
            program_valid: signUpModel.is_program_code_valid,
            address: location.origin,
          });

          axios
            .post("api/Auth/SignUp?" + params, {}, { validateStatus: false })
            .then((res) => {
              if (res.status === 200) {
                isSignupSuccess.value = true;
                buttonStatus.isIdentitySuccess = true;
                cookies.set("tsb-email", signUpModel.email);
                router.push({
                  path: "/verifyemail",
                });
              } else {
                buttonStatus.isIdentityFail = true;
                isSignupSuccess.value = false;
                signupErrorMessage.value = res.data;
              }
            })
            .then(() => {
              isLoading.value = false;
            })
            .finally(() => {
              setTimeout(() => {
                isDisabled.value = false;
                buttonStatus.isIdentitySuccess = false;
                buttonStatus.isIdentityFail = false;
              }, 1000);
            });
        }
      }
      return false;
    }
  }
};

// let timer = "";
const getProgramCode = async () => {
  // clearTimeout(timer);
  // timer = setTimeout(async () => {
  //   programDisabled.value = true;

  // }, 1000);
  await axios
    .get("api/Auth/CheckProgramCode?code=" + signUpModel.program_code + "")
    .then((res) => {
      if (res.status === 200) {
        signUpModel.is_program_code_valid = true;
      } else {
        signUpModel.is_program_code_valid = false;
      }
    });
};

const getPromoCode = async () => {
  // clearTimeout(timer);
  // timer = setTimeout(async () => {
  // }, 1000);
  isPromoCodeValid.value = false;
  await axios
    .get(
      "api/Payment/GetPromoCode?coupon_code=" + signUpModel.promo_code + "",
      {},
      {
        validateStatus: (status) => {
          return status < 500;
        },
      }
    )
    .then((res) => {
      if (res.status === 200) {
        PromoObj.value = [res.data.amountOff, res.data.id];
        isPromoCodeValid.value = true;
      } else {
        PromoObj.value = [];
        isPromoCodeValid.value = false;
      }
    });
};

const checkCardValidity = ($event) => {
  if ($event.error && !$event.empty && $event.complete) {
    cardErrorMessage.value = $event.error.message;
    signUpModel.is_card_valid = false;
  } else {
    if (!$event.empty && $event.complete) {
      cardErrorMessage.value = "";
      signUpModel.is_card_valid = true;
    } else {
      cardErrorMessage.value = "";
      signUpModel.is_card_valid = false;
    }
  }
};

//=============== Stripe config
const stripeKey = ref("pk_test_9zxlqRVBr1X83d6BjLkg1vlQ");
let style = {
  base: {
    color: "#302F2F",
    fontFamily: '"Poppins", sans-serif',
    fontWeight: "500",
    fontSize: "16px",
    "::placeholder": {
      color: "#696969",
      fontWeight: "400",
    },
  },
  invalid: {
    color: "#BF2E3C",
    iconColor: "#BF2E3C",
  },
  complete: {
    iconColor: "#077E3E",
  },
  empty: {
    iconColor: "#BF2E3C",
  },
};
const instanceOptions = reactive({
  // https://stripe.com/docs/js/initializing#init_stripe_js-options
});
const elementsOptions = reactive({
  // https://stripe.com/docs/js/elements_object/create#stripe_elements-options
});
const cardOptions = reactive({
  // https://stripe.com/docs/stripe.js#element-options
  hidePostalCode: true,
  style: style,
  classes: {
    base: "base",
    empty: "empty",
    complete: "complete",
    invalid: "invalid",
  },
});

const stripeLoaded = ref(false);
const card = ref();
const elms = ref();
let socialProviderError = ref("");

onBeforeMount(() => {
  const stripePromise = loadStripe(stripeKey.value);
  stripePromise.then(() => {
    stripeLoaded.value = true;
  });
});

onMounted(() => {
  setInterval(() => {
    testimonyObjIndex.value = Math.trunc(Math.random() * testimonyObj.length);
  }, 3000);
  loadFacebookSDK(document, "script", "facebook-jssdk");
  initFacebook();
});

const gapi = useGapi();

const GoogleAuth = () => {
  isDisabled.value = true;
  isGoogleLoading.value = true;
  socialProviderError.value = "";
  gapi
    .login()
    .then(({ gapi }) => {
      let auth2 = gapi.auth2.getAuthInstance();

      if (auth2.isSignedIn.get()) {
        let convertDurationType = signUpModel.membership_duration ? 1 : 0;
        if (signUpModel.membership_type != "1" && signUpModel.is_card_valid) {
          const cardElement = card.value.stripeElement;
          elms.value.instance.createToken(cardElement).then((result) => {
            if (result != null) {
              const params = queryString.stringify({
                provider_id: auth2.currentUser.get().getBasicProfile().getId(),
                name_user: auth2.currentUser.get().getBasicProfile().getName(),
                role: signUpModel.role,
                token: result.token.id,
                membership_type: parseInt(signUpModel.membership_type),
                coupon: signUpModel.promo_code,
                membership_duration: convertDurationType,
                email: auth2.currentUser.get().getBasicProfile().getEmail(),
                programcode: signUpModel.program_code,
                skip_trial: signUpModel.skip_trial,
                payment_model: signUpModel.payment_model,
                card_valid: signUpModel.is_card_valid,
              });

              axios
                .post(
                  "api/GoogleAuth/GoogleCallback?" + params,
                  {},
                  { validateStatus: false }
                )
                .then((result) => {
                  if (result.status === 200) {
                    cookies.set("tsb-auth", result.data);
                    socialProviderError.value = "";
                    return router.push("/home");
                  } else {
                    socialProviderError.value = result.data;
                  }
                })
                .finally(() => {
                  isDisabled.value = false;
                  isGoogleLoading.value = false;
                });
            }
          });
        } else {
          const params = queryString.stringify({
            provider_id: auth2.currentUser.get().getBasicProfile().getId(),
            name_user: auth2.currentUser.get().getBasicProfile().getName(),
            role: signUpModel.role,
            token: signUpModel.token,
            membership_type: parseInt(signUpModel.membership_type),
            coupon: signUpModel.promo_code,
            membership_duration: convertDurationType,
            email: auth2.currentUser.get().getBasicProfile().getEmail(),
            programcode: signUpModel.program_code,
            skip_trial: signUpModel.skip_trial,
            payment_model: signUpModel.payment_model,
            card_valid: signUpModel.is_card_valid,
          });

          axios
            .post(
              "api/GoogleAuth/GoogleCallback?" + params,
              {},
              { validateStatus: false }
            )
            .then((result) => {
              if (result.status === 200) {
                cookies.set("tsb-auth", result.data);
                socialProviderError.value = "";
                return router.push("/home");
              } else {
                socialProviderError.value = result.data;
              }
            })
            .finally(() => {
              isDisabled.value = false;
              isGoogleLoading.value = false;
            });
        }
      }
    })
    .catch(() => {
      isDisabled.value = false;
      isGoogleLoading.value = false;
    });
};

const FBAuth = () => {
  isDisabled.value = true;
  isFBLoading.value = true;
  socialProviderError.value = "";
  FB.login(
    (response) => {
      if (response.status === "connected") {
        FB.api("/me?fields=id,email,name", (res) => {
          let convertDurationType = signUpModel.membership_duration ? 1 : 0;
          if (
            parseInt(signUpModel.membership_type) > 1 &&
            signUpModel.is_card_valid
          ) {
            const cardElement = card.value.stripeElement;
            elms.value.instance.createToken(cardElement).then((result) => {
              if (result != null) {
                const params = queryString.stringify({
                  provider_id: res.id,
                  name_user: res.name,
                  role: signUpModel.role,
                  token: result.token.id,
                  membership_type: parseInt(signUpModel.membership_type),
                  coupon: signUpModel.promo_code,
                  membership_duration: convertDurationType,
                  email: res.email,
                  programcode: signUpModel.program_code,
                  skip_trial: signUpModel.skip_trial,
                  payment_model: signUpModel.payment_model,
                  card_valid: signUpModel.is_card_valid,
                });
                axios
                  .post(
                    "api/FBAuth/FBCallback?" + params,
                    {},
                    { validateStatus: false }
                  )
                  .then((result) => {
                    if (result.status === 200) {
                      cookies.set("tsb-auth", result.data);
                      socialProviderError.value = "";
                      return router.push("/home");
                    } else {
                      socialProviderError.value = result.data;
                    }
                  })
                  .finally(() => {
                    isDisabled.value = false;
                    isFBLoading.value = false;
                  });
              }
            });
          } else {
            const params = queryString.stringify({
              provider_id: res.id,
              name_user: res.name,
              role: signUpModel.role,
              token: signUpModel.token,
              membership_type: parseInt(signUpModel.membership_type),
              coupon: signUpModel.promo_code,
              membership_duration: convertDurationType,
              email: res.email,
              programcode: signUpModel.program_code,
              skip_trial: signUpModel.skip_trial,
              payment_model: signUpModel.payment_model,
              card_valid: signUpModel.is_card_valid,
            });
            axios
              .post(
                "api/FBAuth/FBCallback?" + params,
                {},
                { validateStatus: false }
              )
              .then((result) => {
                if (result.status === 200) {
                  socialProviderError.value = "";
                  cookies.set("tsb-auth", result.data);
                  return router.push("/home");
                } else {
                  socialProviderError.value = result.data;
                }
              })
              .finally(() => {
                isDisabled.value = false;
                isFBLoading.value = false;
              });
          }
        });
      }
      isDisabled.value = false;
      isFBLoading.value = false;
    },
    { scope: "email" }
  );
  return false;
};

const initFacebook = async () => {
  window.fbAsyncInit = function () {
    FB.init({
      cookie: true,
      appId: "1012317546154792",
      xfbml: true,
      version: "v14.0",
    });
  };
};

const loadFacebookSDK = async (d, s, id) => {
  var js,
    fjs = d.getElementsByTagName(s)[0];
  if (d.getElementById(id)) {
    return;
  }
  js = d.createElement(s);
  js.id = id;
  js.src = "https://connect.facebook.net/en_US/sdk.js";
  fjs.parentNode.insertBefore(js, fjs);
};

const LinkedInAuth = () => {
  let convertDurationType = signUpModel.membership_duration ? 1 : 0;
  isDisabled.value = true;
  isLinkedInLoading.value = true;
  socialProviderError.value = "";
  if (signUpModel.membership_type != "1" && signUpModel.is_card_valid) {
    const cardElement = card.value.stripeElement;
    elms.value.instance.createToken(cardElement).then((result) => {
      if (result != null) {
        const stateAuth = {
          role: signUpModel.role,
          token: result.token.id,
          membership_type: parseInt(signUpModel.membership_type),
          coupon: signUpModel.promo_code,
          membership_duration: convertDurationType,
          programcode: signUpModel.program_code,
          skip_trial: signUpModel.skip_trial,
          payment_model: signUpModel.payment_model,
          card_valid: signUpModel.is_card_valid,
        };
        let stateAuthString = JSON.stringify(stateAuth);
        const { user, session, error } = supabase.auth.signIn(
          {
            provider: "LinkedIn",
          },
          {
            redirectTo:
              location.origin +
              "/linkedinconfirm?refresh=true&state=" +
              stateAuthString +
              "&purpose=signup",
          }
        );
        if (error) {
          alert(error);
          isDisabled.value = false;
          isLinkedInLoading.value = false;
        }
      }
    });
  } else {
    const stateAuth = {
      role: signUpModel.role,
      token: signUpModel.token,
      membership_type: parseInt(signUpModel.membership_type),
      coupon: signUpModel.promo_code,
      membership_duration: convertDurationType,
      programcode: signUpModel.program_code,
      skip_trial: signUpModel.skip_trial,
      payment_model: signUpModel.payment_model,
      card_valid: signUpModel.is_card_valid,
    };
    let stateAuthString = JSON.stringify(stateAuth);
    const { user, session, error } = supabase.auth.signIn(
      {
        provider: "LinkedIn",
      },
      {
        redirectTo:
          location.origin +
          "/linkedinconfirm?refresh=true&state=" +
          stateAuthString +
          "&purpose=signup",
      }
    );
    if (error) {
      alert(error);
      isDisabled.value = false;
      isLinkedInLoading.value = false;
    }
  }
};
</script>

<style scoped>
.membership-label-wrapper {
  position: relative;
}
.subscription-option-content .best-value {
  border-color: #c69030;
}
.subscription-option-content .best-value::before {
  content: "Popular";
  position: absolute;
  top: 0;
  left: 0;
  background: #c69030;
  border-radius: 4px;
  font-size: 12px;
  padding: 4px 8px;
  color: #fff;
  font-weight: bold;
  letter-spacing: 0.25px;
}
.stripe-error-msg {
  font-size: 14px;
}
.content-fade-up-animation {
  animation-duration: 0.5s;
  animation-fill-mode: both;
  -webkit-animation-duration: 0.5s;
  -webkit-animation-fill-mode: both;
  opacity: 0;
  animation-name: fadeInUp;
  -webkit-animation-name: fadeInUp;
}

/* Content fade up animation */

@keyframes fadeInUp {
  from {
    transform: translate3d(0, 30px, 0);
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
}

@-webkit-keyframes fadeInUp {
  from {
    transform: translate3d(0, 30px, 0);
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
}
.payment-model-enter-active {
  transition: all 0.5s ease-out;
}
.payment-model-enter-from {
  opacity: 0;
  transform: translateY(30px);
}
.payment-model-leave-to {
  opacity: 0;
  transform: translateY(-30px);
}

.steps-wrapper__step {
  border-radius: 0.5rem;
  padding: 0.5rem 1rem;
}
.steps-wrapper__step__info .steps-wrapper__step__info__sub {
  font-size: 12px;
  color: #696969;
}
.steps-wrapper__step__info .steps-wrapper__step__info__title {
  font-size: 16px;
}

.card-option .form-check {
  padding: 0px;
  margin: 0px;
}
.content-wrapper .row {
  transition: 0.5s;
}
.content-wrapper .startup-info .s-title,
.content-wrapper .investor-info .s-title {
  font-size: 12px;
}
.card-option {
  height: 100%;
  transition: 0.5s ease;
  cursor: pointer;
}

@media only screen and (min-width: 768px) {
  .card-option {
    max-width: 208px;
  }
}

label {
  display: block;
}
.membership .membership__name,
.membership .membership__duration,
.membership-overview .membership-overview__name,
.membership-info .membership-info__text-info,
.features-list,
.tos-link {
  font-size: 12px;
}

#signup-line {
  text-align: center;
  text-transform: uppercase;
}

#signup-line {
  position: relative;
  z-index: 1;
  width: 100%;
  margin: 1rem auto;
}

#signup-line:before {
  border-top: 2px solid #0f934c;
  content: "";
  margin: 0 auto;
  position: absolute;
  top: 50%;
  left: 0;
  right: 0;
  bottom: 0;
  width: 100%;
  z-index: -1;
}

#signup-line span {
  background: #fafafe;
  padding: 0 5px;
  font-size: 15px;
  color: #0f934c;
  font-weight: 600;
  font-family: lato;
}

#signup-line:before {
  border-top: none;
}

#signup-line:after {
  border-bottom: 2px solid #0f934c;
  content: "";
  margin: 0 auto;
  position: absolute;
  top: 45%;
  left: 0;
  right: 0;
  width: 100%;
  z-index: -1;
}
/*== active tab on step */
.active-tab {
  background: rgba(245, 245, 245, 0.25);
  border: solid 2px #696969;
}
.active-tab .steps-wrapper__step__info__sub {
  font-weight: 600;
  display: block;
}
.active-tab .steps-wrapper__step__info__title {
  font-weight: 600;
  color: #302f2f;
}
/*== valid step*/
.is-valid-tab .steps-wrapper__step__info__sub {
  font-weight: 600;
  display: block;
  color: #3ea528;
}
.is-valid-tab .steps-wrapper__step__info__title {
  font-weight: 600;
  color: #302f2f;
  color: #3ea528;
}
.steps-wrapper__step__info__sub {
  display: none;
}
.steps-wrapper__step__info__title {
  color: #c2c2c2;
}
.card-option.green-1 {
  font-weight: bold;
}
.signup-control .signup-control__show-pass {
  font-size: 12px;
}
</style>