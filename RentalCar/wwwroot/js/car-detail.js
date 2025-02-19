document.getElementById("CarClassId").addEventListener("change", async function () {
    const carClassId = this.value;
    const carTypeSelect = document.getElementById("CarTypeId");

    // CarTypeのoptionをクリア
    carTypeSelect.innerHTML = '';

    if (!carClassId) return;

    try {
        const response = await fetch(`get-car-type?CarClassId=${carClassId}`);
        if (!response.ok) throw new Error("データ取得に失敗しました");

        const data = await response.json();

        // 取得したデータをoptionに追加
        data.forEach(car => {
            const option = document.createElement("option");
            option.value = car.Id;
            option.textContent = car.Name;
            carTypeSelect.appendChild(option);
        });

    } catch (error) {
        console.error("エラー:", error);
    }
});