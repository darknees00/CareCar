// Composables
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/admin',
    name:'admin',
    component: () => import('@/layouts/default/AdminLayout.vue'),
    children: [
      {
        path: "",
        name: "home1",
        component: () => import("../views/admin/HomeAdmin.vue"),
      },
      {
        path: '/admin/booking1',
        name: 'booking1',
        component: () => import('../views/admin/Booking.vue'),
      },
      {
        path: '/admin/service',
        name: 'service1',
        component: () => import('../views/admin/Service.vue'),
      },
      {
        path: '/admin/customer',
        name: 'customer1',
        component: () => import('../views/admin/Customer.vue'),
      },
      {
        path: '/admin/employee',
        name: 'employee',
        component: () => import('../views/admin/Account.vue'),
      },
      {
        path: '/admin/profile-account',
        name: 'profile-account1',
        component: () => import('../views/admin/ProfileAccount.vue'),
      },            
    ],
  },
  {
    path: '/',
    component: () => import('@/layouts/default/UserLayout.vue'),
    children: [
      {
        path: "",
        name: "home",
        component: () => import("@/components/user/Home.vue"),
      },
      {
        path: "/service",
        name: "service",
        component: () => import("@/components/user/ListService.vue"),
      },
      {
        path: "/about-us",
        name: "about-us",
        component: () => import("@/components/user/AboutUs.vue"),
      },
      {
        path: "/contact",
        name: "contact",
        component: () => import("@/components/user/ContactUs.vue"),
      },
      {
        path: "/booking-profile",
        name: "booking-profile",
        component: () => import("@/components/user/BookingProfile.vue"),
      },
      {
        path: "/booking-appointment",
        name: "booking-appointment",
        component: () => import("@/components/user/BookingAppointment.vue"),
      },
    ]
  },

  {
    path: "/admin/login",
    name: "LoginAdmin",
    component: () => import("../views/admin/Login.vue"),
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
