$(function () {
    function request_api_get_status_room() {
        $.post('api.aspx', { action: 'get_status' }, function (data) {
            var json = JSON.parse(data);
            json.forEach(function (room) {
                var roomElement = $('#phong' + room.ma_phong);
                if (roomElement.length) {
                    roomElement.removeClass('dangsua danghoc khonghoc');
                    switch (room.trang_thai) {
                        case 1:
                            roomElement.addClass('khonghoc');
                            break;
                        case 2:
                            roomElement.addClass('danghoc');
                            break;
                        case 3:
                            roomElement.addClass('dangsua');
                            break;
                    }
                }
            });
        });
    }

   
    setInterval(function () { request_api_get_status_room(); }, 1000);

   
});
