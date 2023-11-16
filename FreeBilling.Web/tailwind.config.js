/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
        "./Pages/**/*.{html,cshtml}",
        "./Areas/**/*.{html,cshtml}",
        "./wwwroot/**/*.{html,htm}",
        "../freeBilling.app/src/**.{html,js,vue}"
    ],
  theme: {
    extend: {},
  },
  plugins: [],
}

